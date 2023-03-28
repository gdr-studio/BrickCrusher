using Data.Game;
using UnityEngine;

namespace Merging
{
    public class MergingLogic : MonoBehaviour
    {
        [SerializeField] private MergingDataRepository _repository;
        
        public bool Merge(MergingData data, MergingItemArea area)
        {
            var levelFrom = data.level;
            var levelTo = area.currentData.level;
            if (levelFrom != levelTo)
            {
                return false;
            }
            var newLevel = levelFrom + levelTo;
            var (nextData, newIndex) = _repository.GetNextWithIndex(data);
            if (nextData == null)
            {
                Debug.Log($"no more merging at level: {levelFrom}");
                return false;
            }
            if (nextData.level != newLevel)
            {
                Debug.LogError($"Next data.level != new level: {newLevel}");
                return false;
            }

            var weaponLevel = newIndex + 1;
            if (weaponLevel > GlobalData.CurrentWeaponMaxLevel)
                GlobalData.CurrentWeaponMaxLevel = weaponLevel;
            Debug.Log($"Merged new level, index: {weaponLevel}");
            area.SetData(nextData);
            area.PlayMergeEffect();
            return true;
        }
    }
}