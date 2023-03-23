using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Weapons;

namespace Merging
{
    public partial class MergingInputManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _spawnMask;
        [SerializeField] private LayerMask _cannonsMask;
        [SerializeField] private MergingPurchaser _purchaser;
        [SerializeField] private MergingLogic _mergingLogic;
        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private PlayerWeaponCollection _collection;
        [Space(20)]
        private PointerEventData _pointerEventData;
        public Image movingImage;
        private MovingData _movingData;
        private Camera _camera;

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        private void Awake()
        {
            _movingData = new MovingData();
            _movingData.movable = movingImage.transform;
            _movingData.image = movingImage;
            movingImage.enabled = false;
            IsEnabled = true;
            _camera = Camera.main;
        }
        
        private void Update()
        {
            if (_isEnabled == false)
                return;
            if (Input.GetMouseButtonDown(0))
            {
                var results = RaycastUI();
                ProcessUIOnClick(results);
            }   
            else if (Input.GetMouseButtonUp(0))
            {
                var results = RaycastUI();
                ProcessDrop(results);
            }
            if (Input.GetMouseButton(0))
            {
                if (_movingData.data == null)
                    return;
                Move(Vector3.zero);
                CheckMouseOver();
            }
        }

        private void ProcessUIOnClick(List<RaycastResult> results)
        {
            foreach (var result in results)
            {
                var area = result.gameObject.GetComponent<MergingItemArea>();
                if (area != null)
                {
                    PickFrom(area);
                    return;
                }
            }
            TryRemoveCannon();
        }
        
        private void ProcessDrop(List<RaycastResult> results)
        {
            if (results.Count == 0)
            {
                DropAt(null);
                return;
            }
            foreach (var result in results)
            {
                var area = result.gameObject.GetComponent<MergingItemArea>();
                if (area != null)
                {
                    DropAt(area);
                    return;
                }
                DropAt(null);
            }   
        }

        private void TryRemoveCannon()
        {
            var results = RaycastWorld(_cannonsMask);
            if (results.Length == 0)
                return;
            foreach (var result in results)
            {
                var cannon = result.collider.gameObject.GetComponent<CannonSpawnable>();
                if (cannon != null)
                {
                    _collection.CallRemoveCannon(cannon);
                    cannon.MergingArea.SetDataBack();
                    cannon.MergingArea.PlayReturnEffect();
                    // _purchaser.ReturnMoney(cannon.cannonName);
                }
            }
        }

        private void CheckMouseOver()
        {
            // IS on UI
            if (IsOverUI())
            {
                if (_movingData.IsSpawned)
                {
                    RemoveLastSpawned();
                }
                return;
            }
            // NOT on UI and spawned
            if (_movingData.IsSpawned)
                return;
            // NOT on UI and NOT spawned
            if (_collection.CanSpawn() == false)
                return;
            if (RaycastWorld(_spawnMask).Length > 0)
            {
                Spawn();
            }
        }

        private void RemoveLastSpawned()
        {
            // Debug.Log("is ON ui and spawned");
            _collection.CallRemoveLast();
            _movingData.IsSpawned = false;
        }
        
        private void Spawn()
        {
            _collection.CallSpawnCannon(_movingData.data, _movingData.fromArea);
            _movingData.fromArea.SetSpawned();
            _movingData.IsSpawned = true;
        }

        private bool IsOverUI()
        {
            var results = RaycastUI();
            return results.Count > 0;
        }
        
        private List<RaycastResult> RaycastUI()
        {
            _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            _raycaster.Raycast(_pointerEventData, results);
            return results;
        }

        private RaycastHit[] RaycastWorld(LayerMask mask)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var results = Physics.RaycastAll(ray, 200, mask);
            return results;
        }

        private void PickFrom(MergingItemArea area)
        {
            if (area.IsTaken)
                return;
            var otherData = area.currentData;
            if (otherData == null)
            {
                _purchaser.TryPurchase(area);
                _purchaser.CheckPurchasable();
                return;
            }
            _movingData.fromArea = area;
            _movingData.Show(otherData);
            _movingData.movable.transform.position = Input.mousePosition;
            area.TakeFrom();
        }

        private void DropAt(MergingItemArea area)
        {
            // Dbg.Red("Drop at");
            if (_movingData.data == null)
            {
                _purchaser.CheckPurchasable();
                return;
            }
            if (_movingData.IsSpawned)
            {
                _movingData.HideAndClear();
                _purchaser.CheckPurchasable();
                return;
            }
            if (area == null)
                _movingData.fromArea.SetData(_movingData.data);
            else
            {
                if (area.IsEmpty()) // empty cell
                {
                    area.SetData(_movingData.data);
                    _movingData.fromArea.SetEmpty();
                }
                else
                {
                    if (area == _movingData.fromArea) // same cell
                    {
                        area.SetData(_movingData.data);
                    }
                    else // different cell
                    {
                        var merged = _mergingLogic.Merge(_movingData.data, area);
                        if (!merged)
                            _movingData.fromArea.SetData(_movingData.data);
                        else
                            _movingData.fromArea.SetEmpty();        
                    }
                }
            }
            _movingData.HideAndClear();
            _purchaser.CheckPurchasable();
        }
        
        private void Move(Vector2 moveDir)
        {
            _movingData.movable.transform.position = Input.mousePosition;
        }
    }
}