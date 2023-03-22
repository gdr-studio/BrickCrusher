using System.Collections.Generic;
using UnityEngine;

namespace Merging
{
    public class MergingInitiator : MonoBehaviour
    {
        public List<MergingItemArea> areas = new List<MergingItemArea>();
        public MergingDataRepository repository;
        
        public void Init()
        {
            if(MergingHelpers.CheckFullEmpty(areas))
                areas[0].SetData(repository.GetFirstLevel());
        }
            
    }
}