﻿using System;
using System.Collections.Generic;
using DG.Tweening;
using Merging;
using UnityEngine;
using Weapons;
using Zenject;

namespace Levels.Game
{
    public class WeaponsSpawner : MonoBehaviour
    {
        public float spacing = 1f;
        public int maxCount = 3;
        public float moveToActivePointTime = 0.25f;
        public CannonRepository cannonsRepository;
        public PlayerWeaponCollection collection;
        public Transform spawnPoint;
        public CannonsController cannonsController;
        public Transform mergingPosition;
        [SerializeField] private List<CannonSpawnable> _spawnedCannons;
        [Inject] private DiContainer _container;

        private void OnEnable()
        {
            collection.SpawnCannon = PreStartSpawn;
            collection.RemoveLast = PreStartRemoveLast;
            collection.RemoveCannon = PreStartRemove;
            collection.CheckSpawnAvailable = PreCanSpawn;
            cannonsController.transform.localPosition = mergingPosition.localPosition;
        }
        
        public void InitSpawnedGuns(Action onEnd)
        {
            cannonsController.transform.DOLocalMove(Vector3.zero, moveToActivePointTime);
            foreach (var spawnable in _spawnedCannons)
            {
                cannonsController.cannons.Add(spawnable.cannon);
            }
            cannonsController.Init();
            onEnd?.Invoke();
        }
        
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void PreStartSpawn(MergingData data, MergingItemArea area)
        {
            var count = _spawnedCannons.Count + 1;
            var positions = CalculatePositions(count);
            RepositionSpawned(positions);
            var lastPos = positions[count - 1];
            var spawnable = SpawnCannon(data.cannonName, lastPos);
            spawnable.MergingArea = area;
            collection.SpawnedCount.Val++;
        }

        private void PreStartRemove(CannonSpawnable cannonSpawnable)
        {
            _spawnedCannons.Remove(cannonSpawnable);
            collection.SpawnedCount.Val--;
            cannonSpawnable.Delete();
            if (_spawnedCannons.Count == 0)
                return;
            var positions = CalculatePositions(_spawnedCannons.Count);
            RepositionSpawned(positions);

        }

        private void PreStartRemoveLast()
        {
            var count = _spawnedCannons.Count - 1;
            collection.SpawnedCount.Val--;
            var positions = CalculatePositions(count);
            var lastCannon = _spawnedCannons[count];
            _spawnedCannons.Remove(lastCannon);
            lastCannon.Delete();
            RepositionSpawned(positions);
        }

        private bool PreCanSpawn()
        {
            return _spawnedCannons.Count < maxCount;
        }
        
        
        private void RepositionSpawned(List<Vector3> positions)
        {
            for (int i = 0; i < _spawnedCannons.Count; i++)
            {
                _spawnedCannons[i].Move(positions[i]);
            }
        }
 

        private CannonSpawnable SpawnCannon(CannonName cannonName, Vector3 position)
        {
            var prefab = cannonsRepository.GetPrefab(cannonName);
            var instance = _container.InstantiatePrefabForComponent<Cannon>(prefab, cannonsController.transform);
            instance.transform.position = position;
            instance.transform.rotation = spawnPoint.rotation;
            _spawnedCannons.Add(instance.GetComponent<CannonSpawnable>());
            var spawnable = instance.gameObject.GetComponent<CannonSpawnable>();
            spawnable.Spawn();
            return spawnable;
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
                var worldPos = spawnPoint.TransformPoint(localPos);
                positions.Add(worldPos);
            }
            return positions;
        }
    }
}