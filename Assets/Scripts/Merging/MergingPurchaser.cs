using System;
using System.Collections.Generic;
using Data;
using Helpers;
using Money;
using UnityEngine;

namespace Merging
{
    public class MergingPurchaser : MonoBehaviour
    {
        public event Action<MergingItemArea> OnPurchased;
        
        public int DebugCount = 100;
        public Transform mergableAreasParent;
        public MainGameConfig config;
        public MergingDataRepository repository;
        public List<MergingItemArea> areas;
        
        
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
            var cost = repository.GetPrevLevelCost();
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
            var cost = repository.GetPrevLevelCost();
            if (money < cost)
                return;
            MoneyCounter.TotalMoney.Val -= cost;
            var data = repository.GetPrevLevel();
            area.SetData(data);
            area.PlayBuyEffect();
            OnPurchased?.Invoke(area);
        }
        
        
        
        public void GetParts()
        {
            areas = HierarchyHelpers.GetFromAllChildren<MergingItemArea>(mergableAreasParent, null);
        }
    }
}