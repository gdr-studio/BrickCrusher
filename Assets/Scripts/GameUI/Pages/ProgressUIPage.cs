using UnityEngine;

namespace GameUI
{
    public class ProgressUIPage : UIPage
    {
        [SerializeField] private MoneyUIPage _moneyBlock;
        
        public override void ShowPage(bool fast)
        {
            if (_isOpen)
                return;
            IsOpen = true;
            _moneyBlock.ShowPage(false);
        }
        
    }
}