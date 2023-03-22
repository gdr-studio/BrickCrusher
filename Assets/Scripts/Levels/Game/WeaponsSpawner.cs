using System;
using System.Collections.Generic;
using Merging;
using UnityEngine;
using Weapons;
using Zenject;

namespace Levels.Game
{
    public class WeaponsSpawner : MonoBehaviour
    {
        public float spacing = 1f;
        public CannonRepository cannonsRepository;
        public PlayerWeaponCollection playerWeapons;
        public Transform spawnPoint;
        public CannonsController cannonsController;
        [Inject] private DiContainer _container;
        
        public void SpawnGuns(Action onEnd)
        {
            var positions = CalculatePositions(playerWeapons.currentChoice.Count);
            var i = 0;
            foreach (var data in playerWeapons.currentChoice)
            {
                var position = positions[i];
                SpawnCannon(position, data.cannonName);
                i++;
            }
            cannonsController.Init();
            onEnd?.Invoke();
        }

        private void SpawnCannon(Vector3 position, CannonName cannonName)
        {
            var prefab = cannonsRepository.GetPrefab(cannonName);
            var instance = _container.InstantiatePrefabForComponent<Cannon>(prefab, cannonsController.transform);
            instance.transform.position = position;
            instance.transform.rotation = spawnPoint.rotation;
            cannonsController.cannons.Add(instance);
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