using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Merging
{
    [CreateAssetMenu(fileName = nameof(MergingDataRepository), menuName = "SO/" + nameof(MergingDataRepository))]
    public class MergingDataRepository : ScriptableObject
    {
        public List<MergingData> repository = new List<MergingData>();

        public MergingData GetData(CannonName cannonName)
        {
            return repository.Find(t => t.cannonName == cannonName);
        }
        
        public MergingData GetNext(MergingData data)
        {
            var index = repository.IndexOf(data);
            if (index >= repository.Count - 1)
                return null;
            index++;
            return repository[index];
        }

        public MergingData GetFirstLevel()
        {
            return repository[0];
        }
    }
}