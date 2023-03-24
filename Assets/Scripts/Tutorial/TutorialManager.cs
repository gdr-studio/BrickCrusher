using System;
using System.Collections;
using Data.Game;
using GameUI;
using Merging;
using Tutorial.Hand;
using UnityEngine;
using VFX.Animations.Impl;

namespace Tutorial
{
    public class TutorialManager : UIPage
    {
        public MergingManager merging;
        public MergingInitiator initiator;
        public MergingPurchaser purchaser;
        [Space(10)]
        public TutorHand hand;
        public Transform dropAtPosition;
        public MergingItemArea tutorArea;
        [Space(10)] 
        public float slideScreenPercent;
        public float beginMoveTutorDelay;
        [Space(10)] 
        public PulsingAnimator buyTutorTextAnimator;
        public PulsingAnimator moveTutorTextAnimator;
        public PulsingAnimator mergeTutorTextAnimator;
        [Space(10)]
        public MergingUIPage mergingPage;
        public MoneyUIPage moneyPage;        
        public StartUIPage startPage;
        
        private Coroutine _delayed;
        
        public override void ShowPage(bool fast)
        {
            base.ShowPage(fast);
        }

        public override void HidePage(bool fast)
        {
            base.HidePage(fast);
        }

        public bool PlayedBuyTutor
        {
            get => PlayerPrefs.GetInt("PlayedBuyTutor", 0) != 0;
            set => PlayerPrefs.SetInt("PlayedBuyTutor", value ? 1 : 0);
        }
        
        public bool PlayedMoveTutor
        {
            get => PlayerPrefs.GetInt("PlayedBuyTutor", 0) != 0;
            set => PlayerPrefs.SetInt("PlayedBuyTutor", value ? 1 : 0);
        }
        
        public bool PlayedMergeTutor
        {
            get => PlayerPrefs.GetInt("PlayedBuyTutor", 0) != 0;
            set => PlayerPrefs.SetInt("PlayedBuyTutor", value ? 1 : 0);
        }

        
        public void BeginBuyTutorial()
        {
            ShowPage(true);
            mergingPage.IsOpen = true;
            moneyPage.ShowPage(true);
            hand.Show();
            initiator.InitMoney();
            buyTutorTextAnimator.gameObject.SetActive(true);
            buyTutorTextAnimator.StartScaling();
            purchaser.OnPurchased += OnPurchased;
            MergingManager.OnSpawned += OnSpawned;
            hand.ShowClickAt(tutorArea.CenterPosition);
            PlayedBuyTutor = true;
        }

        public void BeginMovingTutorial()
        {
            GlobalData.CurrentWeapon.OnGrabbed += OnWeaponGrabbed;
            ShowPage(true);
            _delayed = StartCoroutine(Delayed(beginMoveTutorDelay, () =>
            {
                hand.Show();
                var position = GlobalData.CurrentWeapon.Position;
                position = Camera.main.WorldToScreenPoint(position);
                var startPos = position + Vector3.right * (Screen.width * slideScreenPercent);
                hand.ShowToDrag(position, startPos);
                moveTutorTextAnimator.gameObject.SetActive(true);
                moveTutorTextAnimator.StartScaling();
                PlayedMoveTutor = true;
            }));
            
        }

        private void OnWeaponGrabbed()
        {
            if(_delayed != null)
                StopCoroutine(_delayed);
            hand.StopAndHide();
            moveTutorTextAnimator.gameObject.SetActive(false);
            moveTutorTextAnimator.StopScaling();
        }

        public void BeginMergingTutorial()
        {
                
        }
        
        private void OnPurchased(MergingItemArea area)
        {
            buyTutorTextAnimator.StopScaling();
            buyTutorTextAnimator.gameObject.SetActive(false);
            purchaser.OnPurchased -= OnPurchased;
            hand.ShowToDrag(area.transform.position, dropAtPosition.position);
        }

        private void OnSpawned()
        {
            GlobalData.CurrentLevel.OnStarted += OnStarted;
            MergingManager.OnSpawned -= OnSpawned;
            hand.StopAndHide();
            startPage.ShowOnlyStartButton();
        }

        private void OnStarted()
        {
            GlobalData.CurrentLevel.OnStarted -= OnStarted;
            BeginMovingTutorial();
        }


        private IEnumerator Delayed(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }
    }
}