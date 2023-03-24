using System;
using Data;
using Data.Game;
using Game.Sound;
using GameUI;
using Levels;
using PlayerInput;
using Saving;
using Tutorial;
using UnityEngine;
using Zenject;

namespace Main.Boot
{
    public class GameBoot : MonoBehaviour
    {
        public bool showTutor;
        public bool doLoadLevels;
        [SerializeField] private SceneContext _context;
        [SerializeField] private GameDataSaver _dataSaver;
        [SerializeField] private TutorialManager _tutorial;
        
        [Inject] private ILevelManager _levelManager;
        [Inject] private ISoundManager _soundManager;
        [Inject] private IInputManager _input;
        [Inject] private ActionFilter _actions;
        [Inject] private IUIManager _uiManager;
        
        private void Awake()
        {
            _dataSaver.LoadData();

            _context.Run();
            GlobalData.WeaponSlotsMax = 4;
            GlobalData.WeaponSlotsCurrent = 1;
        }

        private void Start()
        {
            _levelManager.Init();
            if(doLoadLevels)
                _levelManager.LoadLast();
            _input.IsEnabled = false;
            _actions.IsEnabled = false;
            _uiManager.Init();
            if (!_tutorial.PlayedBuyTutor || showTutor)
            {
                _tutorial.BeginBuyTutorial();
            }
            else
            {
                _uiManager.ShowStart();
            }
        }

        private void OnDestroy()
        {
            _dataSaver.SaveData();
        }

    }
}