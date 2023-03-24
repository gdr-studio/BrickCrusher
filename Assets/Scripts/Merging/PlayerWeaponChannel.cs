using System;
using System.Dynamic;
using Data;
using React;
using UnityEngine;
using Weapons;

namespace Merging
{
    public delegate bool TrackDelegate(MergingData data, MergingItemArea area);

    [CreateAssetMenu(fileName = nameof(PlayerWeaponChannel), menuName = "SO/" + nameof(PlayerWeaponChannel))]
    public class PlayerWeaponChannel : ScriptableObject
    {
        // public Action<MergingData, MergingItemArea> SpawnCannon;
        public TrackDelegate Track;
        public Action<bool> StopTacking;
        public Action<CannonSpawnable> RemoveCannon;
        public Action RemoveLast;
        public BoolDelegate CheckSpawnAvailable;
        public Action BuyPlacement;

        
        [HideInInspector] public ReactiveProperty<int> SpawnedCount;

        private void OnEnable()
        {
            SpawnedCount = new ReactiveProperty<int>();
        }

        // public void CallSpawnCannon(MergingData data, MergingItemArea fromArea)
        // {
        //     SpawnCannon.Invoke(data, fromArea);
        // }

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