using System.Collections.Generic;
using Data.Game;
using Merging;
using UnityEngine;
using VFX.Animations.Impl;
using Zenject;

namespace GameUI
{
    public class StartUIPage : UIPageWithButton
    {
        [Inject] private IUIManager _uiManager;
        [SerializeField] private List<PulsingAnimator> _animators;
        [SerializeField] private MergingUIPage _mergingUI;
        [SerializeField] private PlayerWeaponChannel channel;
        [SerializeField] private MoneyUIPage _moneyUIPage;
        [SerializeField] private PulsingAnimator _startButtonScaler;
        [SerializeField] private float _buttonScalingTime = 0.35f;
        private bool _shownButton = true;
        
        public override void ShowPage(bool fast)
        {
            if (IsOpen)
                return;
            base.ShowPage(fast);
            // _button.interactable = true;
            _button.OnDown += OnClick;
            foreach (var animator in _animators)
                animator.StartScaling();
            _mergingUI.ShowPage(false);
            _moneyUIPage.ShowPage(false);
            // _button.gameObject.SetActive(false);
            OnSpawnedCount(0);
            channel.SpawnedCount.SubOnChange(OnSpawnedCount);
        }

        public override void HidePage(bool fast)
        {
            if (IsOpen == false)
                return;
            base.HidePage(fast);
            _button.OnDown -= OnClick;
            _button.interactable = false;
            _mergingUI.HidePage(false);
            _startButtonScaler.StopScaling();
            channel.SpawnedCount.UnsubOnChange(OnSpawnedCount);
        }

        public void ShowOnlyStartButton()
        {
            IsOpen = true;
            _button.OnDown += OnClick;
            ShowStartButton();
        }
        
        public override void OnClick()
        {
            _uiManager.ShowProgress();
            if (GlobalData.CurrentLevel != null)
            {
                GlobalData.CurrentLevel.StartLevel();
            }
            else
            {
                Debug.Log("no start level");
            }
        }

        public override void SetHeader(string text)
        {
            _header.text = text;
        }

        private void ShowStartButton()
        {
            _shownButton = true;
            _startButtonScaler.ShowScaling(_buttonScalingTime, () =>
            {
                _button.interactable = true;
                _startButtonScaler.StartScaling();
            });
        }

        private void HideButton()
        {
            _shownButton = false;
            _button.interactable = false;
            _startButtonScaler.HideScaling(0f, () =>{});       
        }
        
        private void OnSpawnedCount(int count)
        {
            if (count > 0)
            {
                if (_shownButton) 
                    return;
                ShowStartButton();
            }
            else
            {
                if (!_shownButton )
                    return;
                HideButton();
            }
        }


    }
}