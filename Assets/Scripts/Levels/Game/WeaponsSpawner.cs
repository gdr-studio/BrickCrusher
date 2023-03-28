using System;
using System.Collections.Generic;
using Data.Game;
using DG.Tweening;
using Merging;
using UnityEngine;
using Weapons;
using Zenject;

namespace Levels.Game
{
    public partial class WeaponsSpawner : MonoBehaviour
    {
        public float spacing = 1f;
        public float moveToActivePointTime = 0.25f;
        public float bigScale = 1.5f;
        public float normalScale = 1f;
        public CannonPlacementCost placementCosts;
        public CannonRepository cannonsRepository;
        public PlayerWeaponChannel channel;
        public CannonsController cannonsController;
        public Transform mergingPosition;
        public Vector3 signPositionOffset;
        [SerializeField] private List<SpawnArea> _spawnedData;
        private Camera _cam;
        private TackingData _currentTrackData;
        private PlacementBuySign _buySign;
        [Inject] private DiContainer _container;
        
        public void InitSpawnedGuns(Action onEnd)
        {
            cannonsController.transform.DOLocalMove(Vector3.zero, moveToActivePointTime);
            cannonsController.transform.DOScale(Vector3.one * normalScale, moveToActivePointTime);
            foreach (var spawnArea in _spawnedData)
            {
                if (spawnArea.IsFree)
                {
                    spawnArea.Placement.Hide();
                    continue;
                }
                cannonsController.cannons.Add(spawnArea.SpawnedCannon.cannon);
            }

            if (_buySign != null)
            {
                _buySign.Hide();
            }
            cannonsController.Init();
            onEnd?.Invoke();
        }

        private void OnEnable()
        {
            cannonsController.transform.localScale = Vector3.one * bigScale;
            channel.Track = Track;
            channel.StopTacking = StopTracking;
            channel.RemoveCannon = PreStartRemove;
            channel.CheckSpawnAvailable = CheckCanSpawn;
            channel.BuyPlacement = BuyNewArea;
            channel.SpawnedCount.Val = 0;
            
            _cam = Camera.main;
            cannonsController.transform.localPosition = mergingPosition.localPosition;
            SpawnPlacements();
        }

        private void OnDisable()
        {
            if (_spawnedData == null)
                return;
            foreach (var data in _spawnedData)
            {
                if(data.Placement != null)
                    CannonPlacementManager.ReturnOne(data.Placement);
            }
        }

        private void SpawnPlacements()
        {
            var count = GlobalData.WeaponSlotsCurrent;
            if (count == 0)
                return;
            var positions = CalculatePositions(count);

            if (count < GlobalData.WeaponSlotsMax)
            {
                var position = positions[positions.Count - 1] + signPositionOffset;
                _buySign = CannonPlacementManager.GetBuySign();
                _buySign.transform.position = position;
                _buySign.Show();
            }
            _spawnedData = new List<SpawnArea>();
            for (int i = 0; i < count; i++)
            {
                var placement = CannonPlacementManager.GetOne();
                placement.transform.localScale = bigScale * Vector3.one;
                placement.transform.position = positions[i];
                placement.Show();
                _spawnedData.Add(new SpawnArea()
                {
                    Position =  positions[i],
                    ScreenPosition = _cam.WorldToScreenPoint(positions[i]),
                    Placement = placement,
                    IsFree = true
                });
            }
        }


        private bool Track(MergingData data, MergingItemArea area)
        {
            if (_currentTrackData == null)
            {
                _currentTrackData = new TackingData();
                _currentTrackData.fromArea = area;
                _currentTrackData.mergingData = data;
                SpawnTracked();
                return true;
            }
            return MoveTracked();
        }

        private void StopTracking(bool spawn)
        {
            if (_currentTrackData == null)
                return;
            if (spawn)
            {
                // Dbg.Green("dropped and spawned");
            }
            else
            {
                // Dbg.Yellow("not spawned, stop tracking");
                if (_currentTrackData.currentArea != null)
                {
                    if (_currentTrackData.currentArea.IsFree == false)
                    {
                        _currentTrackData.currentArea.Free();
                        channel.SpawnedCount.Val--;
                    }
                }
            }
            _currentTrackData = null;
        }
        
        private void SpawnTracked()
        {
            _currentTrackData.currentArea?.Free();
            var spawnArea = GetClosestFreeArea();
            if (spawnArea.IsFree == false)
                return;
            var spawnable = SpawnCannon(_currentTrackData.mergingData.cannonName, spawnArea.Position);
            spawnable.Spawn();
            spawnable.MergingArea = _currentTrackData.fromArea;
            _currentTrackData.Spawnable = spawnable;
            _currentTrackData.SetArea(spawnArea);
        }

        private bool MoveTracked()
        {
            var spawnArea = GetClosestFree();
            if (spawnArea != _currentTrackData.currentArea)
            {
                if (spawnArea.IsFree == false)
                    return false;
                _currentTrackData.currentArea.FreeNoDel();
                _currentTrackData.SetArea(spawnArea);
            }
            return true;
        }

        private void PreStartRemove(CannonSpawnable cannonSpawnable)
        {
            var area = _spawnedData.Find(t => t.SpawnedCannon == cannonSpawnable);
            area.Free();
            _currentTrackData = null;
            channel.SpawnedCount.Val--;
        }

        private bool CheckCanSpawn()
        {
            // Dbg.Green($"Spawned: {channel.SpawnedCount.Val}, max: {GlobalData.WeaponSlotsCurrent}" +
            //           $"CAN SPAWN? : {channel.SpawnedCount.Val < GlobalData.WeaponSlotsCurrent}");
            return channel.SpawnedCount.Val < GlobalData.WeaponSlotsCurrent;
        }


        // ReSharper disable Unity.PerformanceAnalysis
        private CannonSpawnable SpawnCannon(CannonName cannonName, Vector3 position)
        {
            var prefab = cannonsRepository.GetPrefab(cannonName);
            var instance = _container.InstantiatePrefabForComponent<Cannon>(prefab, cannonsController.transform);
            instance.transform.position = position;
            instance.transform.rotation = cannonsController.transform.rotation;
            var spawnable = instance.gameObject.GetComponent<CannonSpawnable>();
            spawnable.Spawn();
            channel.SpawnedCount.Val++;
            return spawnable;
        }
        
        private void BuyNewArea()
        {
            if (placementCosts.CheckCanBuy() == false)
                return;
            placementCosts.BuyNext();
            var count = GlobalData.WeaponSlotsCurrent;
            var positions = CalculatePositions(count);
            var lastPos = positions[positions.Count - 1];

            for (int i = 0; i < _spawnedData.Count; i++)
            {
                var data = _spawnedData[i];
                data.Position = positions[i];
                data.ScreenPosition = _cam.WorldToScreenPoint(positions[i]);
                // data.Placement.transform.position = positions[i];
                data.Placement.spawnable.Move(positions[i]);
                if(data.IsFree == false)
                    data.SpawnedCannon.Move(positions[i]);
            }
            var newPlacement = CannonPlacementManager.GetOne();
            newPlacement.transform.position = lastPos;
            newPlacement.transform.localScale = bigScale * Vector3.one;
            newPlacement.Show();
            _spawnedData.Add(new SpawnArea()
            {
                Placement = newPlacement,
                Position = lastPos,
                ScreenPosition = _cam.WorldToScreenPoint(lastPos),
                IsFree = true
            });
            
            if (count < GlobalData.WeaponSlotsMax)
            {
                var position = lastPos + signPositionOffset;
                _buySign = CannonPlacementManager.GetBuySign();
                _buySign.transform.position = position;
                _buySign.Show();
            }
            else
            {
                _buySign.Hide();
            }
        }
        
        
        
        private List<Vector3> CalculatePositions(int count)
        {
            var positions = new List<Vector3>();
            var center = (count / 2 * spacing) * Vector3.right;
            if (count % 2 == 0)
                center -= spacing / 2 * Vector3.right;
            for (int i = 0; i < count; i++)
            {
                var localPos = Vector3.zero + spacing * i * Vector3.right - center;
                var worldPos = cannonsController.transform.TransformPoint(localPos);
                positions.Add(worldPos);
            }
            return positions;
        }

        private SpawnArea GetClosestFree()
        {
            var shortest = float.MaxValue;
            var areaRes = _spawnedData[0];
            var inputPos = Input.mousePosition;
            foreach (var area in _spawnedData)
            {
                var d = (inputPos - area.ScreenPosition).sqrMagnitude;
                if (d <= shortest)
                {
                    shortest = d;
                    areaRes = area;
                }
            }  
            return areaRes;
        }

        private SpawnArea GetClosestFreeArea()
        {
            var shortest = float.MaxValue;
            var areaRes = _spawnedData[0];
            var inputPos = Input.mousePosition;
            foreach (var area in _spawnedData)
            {
                var d = (inputPos - area.ScreenPosition).sqrMagnitude;
                if (d <= shortest && area.IsFree)
                {
                    shortest = d;
                    areaRes = area;
                }
            }  
            // Debug.Log($"Shortes index: {resIndex}");
            return areaRes;
        }
    }
}