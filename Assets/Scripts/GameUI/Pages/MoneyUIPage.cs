using Money;
using UnityEngine;

namespace GameUI
{
    public class MoneyUIPage : UIPage
    {
        [SerializeField] private MoneyBlock _moneyBlock;
        public override void ShowPage(bool fast)
        {
            if (_isOpen)
                return;
            IsOpen = true;
            _moneyBlock.SetCount();
            MoneyCounter.TotalMoney.SubOnChange(OnMoneyChange);
        }
        
        public override void HidePage(bool fast)
        {
            _canvas.enabled = false;
            MoneyCounter.TotalMoney.UnsubOnChange(OnMoneyChange);
        }
        
        private void OnMoneyChange(float obj)
        {
            _moneyBlock.UpdateCount();
        }
    }
}