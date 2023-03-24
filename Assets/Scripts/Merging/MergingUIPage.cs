using GameUI;
using UnityEngine;

namespace Merging
{
    public class MergingUIPage : UIPage
    {
        [SerializeField] private MergingPurchaser _purchaser;
        [SerializeField] private MergingInitiator _mergingInitiator;
        
        public override void ShowPage(bool fast)
        {
            if (_isOpen)
                return;
            base.ShowPage(fast);
            _mergingInitiator.Init();
            _purchaser.Init();
            MergingManager.IsEnabled = true;
        }
        
        
    }
}