using ImageToVolume;
using UnityEngine;

namespace Money
{
    public class BrokenBlockMoney : MonoBehaviour, IBrokenBlock
    {
        public int moneyCount = 1;
        public ElementSub sub;

        public int GetMoneyAndHide()
        {
            sub.HideAndReturn();
            return moneyCount;
        }
    }
}