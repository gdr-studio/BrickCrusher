using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Weapons;

namespace Merging
{
    [CreateAssetMenu(fileName = nameof(PlayerWeaponCollection), menuName = "SO/" + nameof(PlayerWeaponCollection))]
    public class PlayerWeaponCollection : ScriptableObject
    {
        public Action<MergingData> SpawnCannon;
        public Action<Cannon> RemoveCannon;
        public Action RemoveLast;
        public BoolDelegate CheckSpawnAvailable;
        
        public List<MergingData> currentChoice;

        public void CallSpawnCannon(MergingData data)
        {
            SpawnCannon.Invoke(data);
        }

        public void CallRemoveCannon(Cannon cannon)
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