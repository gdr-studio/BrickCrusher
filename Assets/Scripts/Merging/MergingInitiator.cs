using System.Collections.Generic;
using Data;
using Money;
using UnityEngine;

namespace Merging
{
    public class MergingInitiator : MonoBehaviour
    {
        public List<MergingItemArea> areas;
        public MergingDataRepository repository;
        public MergingPurchaser purchaser;
        public MainGameConfig config;
        
        public void Init()
        {
            if (MergingHelpers.CheckFullEmpty(areas))
            {
                InitMoney();
                purchaser.OnPurchased += OnPurchased;
                areas[0].Highlight();
            }
        }

        public void InitMoney()
        {
            var money = MoneyCounter.TotalMoney.Val;
            var cost = config.FirstLevelCannonCost;
            if (money < cost)
            {
                MoneyCounter.TotalMoney.Val = cost;
            }            
        }

        private void OnPurchased(MergingItemArea obj)
        {
            purchaser.OnPurchased -= OnPurchased;
            areas[0].StopHighlight();
        }
    }
}