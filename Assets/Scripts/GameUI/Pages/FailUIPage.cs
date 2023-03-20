using System.Collections.Generic;
using JetBrains.Annotations;
using Levels;
using UnityEngine;
using VFX.Animations.Impl;
using Zenject;

namespace GameUI
{
    public class FailUIPage : UIPageWithButton
    {
        [Inject] private LevelManager _levelManager;
        [Inject] private IUIManager _uiManager;
        [SerializeField] private List<PulsingAnimator> _animators;
        
        public override void ShowPage(bool fast)
        {
            base.ShowPage(fast);
            _button.onClick.AddListener(OnClick);
            _button.interactable = true;
            _header.text = $"LEVEL {_levelManager.TotalLevels + 1} FAILED";
            foreach (var animator in _animators)
            {
                animator.StartScaling();
            }
        }

        public override void HidePage(bool fast)
        {
            base.HidePage(fast);
            _button.onClick.RemoveListener(OnClick);
            _button.interactable = false;
        }

        public override void SetHeader(string text)
        {
            _header.text = text;
        }

        public override void OnClick()
        {
            _levelManager.RestartLevel();
            _uiManager.ShowStart();
        }
    }
}