using System;
using System.Collections.Generic;
using Data;
using Helpers;
using Money;
using Unity.VisualScripting;
using UnityEngine;

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
            CheckPurchase();
        }
        
        public void CheckPurchase()
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
                if (area.IsEmpty())
                {
                    area.HideCost();
                }
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


        public void GetParts()
        {
            areas = HierarchyHelpers.GetFromAllChildren<MergingItemArea>(mergableAreasParent, null);
        }
    }
}