using System.Collections.Generic;
using Data.Game;
using Money;
using UnityEngine;

namespace Merging
{
    [CreateAssetMenu(fileName = nameof(CannonPlacementCost), menuName = "SO/" + nameof(CannonPlacementCost))]
    public class CannonPlacementCost : ScriptableObject
    {
        public List<int> costs;

        public int GetNextCost()
        {
            var index = GlobalData.WeaponSlotsCurrent - 2;
            // Debug.Log($"[GetCost], slots = {GlobalData.WeaponSlotsCurrent }, index: {index}");
            index++;
            if (index < costs.Count)
                return costs[index];
            return costs[costs.Count - 1];
        }

        public bool CheckCanBuy()
        {
            if (GlobalData.WeaponSlotsCurrent >= GlobalData.WeaponSlotsMax)
                return false;
            var cost = GetNextCost();
            if (MoneyCounter.TotalMoney.Val >= cost)
            {
                Debug.Log($"Money: {MoneyCounter.TotalMoney.Val}, cost: {cost}");
                return true;
            }
            return false;
        }

        public void BuyNext()
        {
            var cost = GetNextCost();
            GlobalData.WeaponSlotsCurrent++;
            MoneyCounter.TotalMoney.Val -= cost;
        }
    }
}