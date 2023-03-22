using System.Collections.Generic;
using UnityEngine;

namespace Merging
{
    [CreateAssetMenu(fileName = nameof(PlayerWeaponCollection), menuName = "SO/" + nameof(PlayerWeaponCollection))]
    public class PlayerWeaponCollection : ScriptableObject
    {
        public List<MergingData> currentChoice;
        
        
    }
}