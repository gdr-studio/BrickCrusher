using System.Collections.Generic;
using Data.Game;
using UnityEngine;
using VFX.Animations.Impl;
using Zenject;

namespace GameUI
{
    public class StartUIPage : UIPageWithButton
    {
        [Inject] private IUIManager _uiManager;
        [SerializeField] private List<PulsingAnimator> _animators;

        public override void ShowPage(bool fast)
        {
            base.ShowPage(fast);
            _button.interactable = true;
            _button.OnDown += OnClick;
            foreach (var animator in _animators)
            {
                animator.StartScaling();
            }
        }

        public override void HidePage(bool fast)
        {
            base.HidePage(fast);
            _button.OnDown -= OnClick;
            _button.interactable = false;
            foreach (var animator in _animators)
            {
                animator.StopScaling();
            }
        }

        public override void OnClick()
        {
            GlobalData.CurrentLevel.StartLevel();
            _uiManager.ShowProgress();
            
        }

        public override void SetHeader(string text)
        {
            _header.text = text;
        }

    }
}