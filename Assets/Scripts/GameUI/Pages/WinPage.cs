using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Helpers;
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
        // [Inject] private IUIMoney _money;

        [SerializeField] private float _moneySendDelay = 1f;
        [SerializeField] private Transform _moneyFromPos;
        [SerializeField] private TextMeshProUGUI _bonusText;
        [SerializeField] private List<PulsingAnimator> _animators;
        private Coroutine _delayedMoneySent;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void ShowPage(bool fast)
        {
            base.ShowPage(fast);
            _button.onClick.AddListener(OnClick);
            _button.interactable = true;
            _header.text = $"LEVEL {_levelManager.TotalLevels + 1} COMPLETED";
            foreach (var animator in _animators)
                animator.StartScaling();
            // var moneyWithBonus = GameMoney.LevelMoney * GameMoney.Multiplier;
            // _bonusText.text = $"{GameMoney.LevelMoney}$ x {GameMoney.Multiplier} = {moneyWithBonus}$";
            // var addCount = (int)moneyWithBonus - GameMoney.LevelMoney;
            // if (addCount > 0)
            // {
            //     GameMoney.LevelMoney += addCount;
            //     if(_delayedMoneySent != null)
            //         StopCoroutine(_delayedMoneySent);
            //     _delayedMoneySent = StartCoroutine(Delayed(() => {   _money.SpawnDollars((Vector2)_moneyFromPos.position, addCount);}, _moneySendDelay));
            // }
          
        }

        public override void HidePage(bool fast)
        {
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