using System.Collections.Generic;
using Data;
using Data.Game;
using UnityEngine;
using Weapons;

namespace Merging
{
    [CreateAssetMenu(fileName = nameof(MergingDataRepository), menuName = "SO/" + nameof(MergingDataRepository))]
    public class MergingDataRepository : ScriptableObject
    {
        public MainGameConfig config;
        public List<MergingData> repository = new List<MergingData>();

        public int GetPrevLevelCost()
        {
            var index = GetCurrentWeaponIndex();
            if (index == 0)
                return repository[0].level * config.FirstLevelCannonCost;
            return repository[index - 1].level * config.FirstLevelCannonCost;
        }

        public MergingData GetPrevLevel()
        {
            var index = GetCurrentWeaponIndex();
            if (index == 0)
                return repository[0];
            return repository[index - 1];   
        }

        private int GetCurrentWeaponIndex()
        {
            var index = GlobalData.CurrentWeaponMaxLevel - 1;
            if (index >= repository.Count || index < 0)
            {
                Debug.LogError($"Error in weapon level index: {index}");
                return -1;
            }

            return index;
        }
        
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
        
        public (MergingData,int) GetNextWithIndex(MergingData data)
        {
            var index = repository.IndexOf(data);
            if (index >= repository.Count - 1)
                return (null, -1);
            index++;
            return (repository[index], index);
        }

        public MergingData GetFirstLevel()
        {
            return repository[0];
        }
    }
}