using GameUI;
using Helpers;
using UnityEngine;

namespace Merging
{
    public class MergingUIPage : UIPage
    {
        [SerializeField] private MergingPurchaser _purchaser;
        [SerializeField] private MergingInputManager _inputManager;
        [SerializeField] private MergeActiveRow _activeRow;
        [SerializeField] private MergingInitiator _mergingInitiator;

        public MergeActiveRow MergeActiveRow => _activeRow;
        
        public override void ShowPage(bool fast)
        {
            if (_isOpen)
                return;
            base.ShowPage(fast);
            _mergingInitiator.Init();
            _purchaser.Init();
            _inputManager.IsEnabled = true;
        }
        
        
    }
}