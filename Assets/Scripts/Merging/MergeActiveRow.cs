using System.Collections.Generic;
using React;
using UnityEngine;

namespace Merging
{
    public class MergeActiveRow : MonoBehaviour
    {
        public List<MergingItemArea> areas = new List<MergingItemArea>();
        public ReactiveProperty<bool> hasChosen = new ReactiveProperty<bool>();
        public PlayerWeaponCollection playerCollection;

        public bool IsEmpty => MergingHelpers.CheckFullEmpty(areas);
        private void OnEnable()
        {
            foreach (var area in areas)
            {
                area.HideCost();
                area.OnDataSet += OnDataSet;
            }
        }

        private void OnDataSet(MergingItemArea area)
        {
            if (MergingHelpers.CheckFullEmpty(areas))
            {
                hasChosen.Val = false;
            }
            else
            {
                hasChosen.Val = true;
            }
            ResetCollection();
        }

        private void ResetCollection()
        {
            var current = new List<MergingData>();
            foreach (var area in areas)
            {
                if(area.IsEmpty() == false)
                    current.Add(area.currentData);
            }
            playerCollection.currentChoice = current;
        }
    }
}