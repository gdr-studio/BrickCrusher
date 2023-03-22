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
            var nextData = _repository.GetNext(data);
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
            area.SetData(nextData);
            return true;
        }
    }
}