using Money;
using UnityEngine;

namespace GameUI
{
    public class MoneyUIPage : UIPage
    {
        [SerializeField] private MoneyBlock _moneyBlock;
        public override void ShowPage(bool fast)
        {
            if (IsOpen)
                return;
            IsOpen = true;
            _moneyBlock.SetCount();
            MoneyCounter.TotalMoney.SubOnChange(OnMoneyChange);
        }
        
        public override void HidePage(bool fast)
        {
            if (IsOpen == false)
                return;
            IsOpen = false;
            MoneyCounter.TotalMoney.UnsubOnChange(OnMoneyChange);
            _moneyBlock.Stop();
            _moneyBlock.SetCount();
        }
        
        private void OnMoneyChange(int obj)
        {
            _moneyBlock.UpdateCount();
        }
    }
}