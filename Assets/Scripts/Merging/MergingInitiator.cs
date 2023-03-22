using System.Collections.Generic;
using UnityEngine;

namespace Merging
{
    public class MergingInitiator : MonoBehaviour
    {
        public List<MergingItemArea> areas = new List<MergingItemArea>();
        public MergeActiveRow activeRow;
        public MergingDataRepository repository;
        
        public void Init()
        {
            if(MergingHelpers.CheckFullEmpty(areas) && activeRow.IsEmpty)
                areas[0].SetData(repository.GetFirstLevel());
        }
            
    }
}