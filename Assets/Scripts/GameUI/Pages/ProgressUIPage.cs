using Data;
using Money;
using Services.Pool;
using UnityEngine;
using Zenject;

namespace GameUI
{
    public class ProgressUIPage : UIPage
    {
        [SerializeField] private MoneyBlock _moneyBlock;
        private Camera _camera;
        
        public override void ShowPage(bool fast)
        {
            if (_isOpen)
                return;
            IsOpen = true;
            // _camera = Camera.main;
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