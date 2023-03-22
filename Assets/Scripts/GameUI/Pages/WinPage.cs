using System;
using System.Collections;
using System.Collections.Generic;
using Levels;
using TMPro;
using UnityEngine;
using VFX.Animations.Impl;
using Zenject;

namespace GameUI
{
    public class WinPage : UIPageWithButton
    {
        [Inject] private LevelManager _levelManager;
        [Inject] private IUIManager _uiManager;

        [SerializeField] private List<PulsingAnimator> _animators;
        private Coroutine _delayedMoneySent;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void ShowPage(bool fast)
        {
            if (IsOpen)
                return;
            base.ShowPage(fast);
            _button.onClick.AddListener(OnClick);
            _button.interactable = true;
            _header.text = $"LEVEL {_levelManager.TotalLevels + 1} COMPLETED";
            foreach (var animator in _animators)
                animator.StartScaling();

        }

        public override void HidePage(bool fast)
        {
            if (IsOpen == false)
                return;
            if(_delayedMoneySent != null)
                StopCoroutine(_delayedMoneySent);
            base.HidePage(fast);
            _button.onClick.RemoveListener(OnClick);
            _button.interactable = false;
        }

        public override void OnClick()
        {
            _levelManager.NextLevel();
            _uiManager.ShowStart();
        }

        private IEnumerator Delayed(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }

    }
}