using System;
using Data;
using React;
using UnityEngine;
using Weapons;

namespace Merging
{
    [CreateAssetMenu(fileName = nameof(PlayerWeaponCollection), menuName = "SO/" + nameof(PlayerWeaponCollection))]
    public class PlayerWeaponCollection : ScriptableObject
    {
        public Action<MergingData, MergingItemArea> SpawnCannon;
        public Action<CannonSpawnable> RemoveCannon;
        public Action RemoveLast;
        public BoolDelegate CheckSpawnAvailable;
        [HideInInspector] public ReactiveProperty<int> SpawnedCount;

        private void OnEnable()
        {
            SpawnedCount = new ReactiveProperty<int>();
        }

        public void CallSpawnCannon(MergingData data, MergingItemArea fromArea)
        {
            SpawnCannon.Invoke(data, fromArea);
        }

        public void CallRemoveCannon(CannonSpawnable cannon)
        {
            RemoveCannon.Invoke(cannon);
        }

        public void CallRemoveLast()
        {
            RemoveLast.Invoke();
        }

        public bool CanSpawn() => CheckSpawnAvailable.Invoke();
    }
}