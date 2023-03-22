using GameUI;
using Helpers;
using UnityEngine;

namespace Merging
{
    public class MergingUIPage : UIPage
    {
        [SerializeField] private MergingPurchaser _purchaser;
        [SerializeField] private MergingInputManager _inputManager;
        [SerializeField] private ActivateRow _activeRow;
        [SerializeField] private MergingInitiator _mergingInitiator;

        public ActivateRow ActivateRow => _activeRow;
        
        public override void ShowPage(bool fast)
        {
            base.ShowPage(fast);
            _mergingInitiator.Init();
            _purchaser.Init();
            _inputManager.IsEnabled = true;
        }
        
        
    }
}