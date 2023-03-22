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
        [SerializeField] private PlayerWeaponCollection _collection;
        [SerializeField] private MoneyUIPage _moneyUIPage;
        [SerializeField] private PulsingAnimator _startButtonScaler;
        [SerializeField] private float _buttonScalingTime = 0.35f;
        
        public override void ShowPage(bool fast)
        {
            if (IsOpen)
                return;
            base.ShowPage(fast);
            _button.interactable = true;
            _button.OnDown += OnClick;
            foreach (var animator in _animators)
                animator.StartScaling();
            _mergingUI.ShowPage(false);
            _moneyUIPage.ShowPage(false);
            // _button.gameObject.SetActive(false);
            OnSpawnedCount(0);
            _collection.SpawnedCount.SubOnChange(OnSpawnedCount);
        }

        public override void HidePage(bool fast)
        {
            if (IsOpen == false)
                return;
            base.HidePage(fast);
            _button.OnDown -= OnClick;
            _button.interactable = false;
            foreach (var animator in _animators)
            {
                animator.StopScaling();
            }
            _mergingUI.HidePage(false);
            _collection.SpawnedCount.UnsubOnChange(OnSpawnedCount);
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
        
        private void OnSpawnedCount(int count)
        {
            if (count > 0)
            {
                _startButtonScaler.ShowScaling(_buttonScalingTime, () =>
                {
                    _button.interactable = true;
                    _startButtonScaler.StartScaling();
                });
            }
            else
            {
                _button.interactable = false;
                _startButtonScaler.HideScaling(0f, () =>
                {
                });   
            }
        }


    }
}