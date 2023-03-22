using System.Collections.Generic;
using Data;
using Helpers;
using Money;
using UnityEngine;
using Weapons;

namespace Merging
{
    public class MergingPurchaser : MonoBehaviour
    {
        public int DebugCount = 100;
        public Transform mergableAreasParent;
        public MainGameConfig config;
        public MergingDataRepository repository;
        public List<MergingItemArea> areas = new List<MergingItemArea>();
        private void OnEnable()
        {
            if(DebugCount > 0)
                MoneyCounter.TotalMoney.Val = DebugCount;
            Init();
        }

        public void Init()
        {
            CheckPurchasable();
        }
        
        public void CheckPurchasable()
        {
            var money = MoneyCounter.TotalMoney.Val;
            var cost = config.FirstLevelCannonCost;
            if (money < cost)
            {
                HideAll();
            }
            else
            {
                ShowAvailable(cost);
            }
        }

        private void HideAll()
        {
            foreach (var area in areas)
            {
                area.HideCost();
            }      
        }
        
        private void ShowAvailable(int cost)
        {
            foreach (var area in areas)
            {
                if (area.IsEmpty())
                {
                    area.ShowCost(cost);
                }
                else
                {
                    area.HideCost();
                }
            }      
        }

        public void TryPurchase(MergingItemArea area)
        {
            var money = MoneyCounter.TotalMoney.Val;
            var cost = config.FirstLevelCannonCost;
            if (money < cost)
                return;
            MoneyCounter.TotalMoney.Val -= cost;
            var data = repository.GetFirstLevel();
            area.SetData(data);
        }

        public void ReturnMoney(CannonName cannonName)
        {
            var data = repository.GetData(cannonName);
            var level = data.level;
            var money = config.FirstLevelCannonCost * level;
            Dbg.Green($"returning money. level: {level}, total {money}");
            MoneyCounter.TotalMoney.Val += money;
        }
        
        
        
        public void GetParts()
        {
            areas = HierarchyHelpers.GetFromAllChildren<MergingItemArea>(mergableAreasParent, null);
        }
    }
}