using UnityEngine;

namespace Money
{
    public class MoneyCheat : MonoBehaviour
    {
        public int addAmount;

        public void Add()
        {
            MoneyCounter.LevelMoney.Val += addAmount;
            MoneyCounter.TotalMoney.Val += addAmount;
        }

        public void Clear()
        {
            MoneyCounter.Clear();
        }
    }
}