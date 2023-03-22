using System;
using Game.Sound;
using GameUI;
using Levels;
using PlayerInput;
using Saving;
using UnityEngine;
using Zenject;

namespace Main.Boot
{
    public class GameBoot : MonoBehaviour
    {
        public bool doLoadLevels;
        [SerializeField] private SceneContext _context;
        [SerializeField] private GameDataSaver _dataSaver;
        [Inject] private ILevelManager _levelManager;
        [Inject] private ISoundManager _soundManager;
        [Inject] private IInputManager _input;
        [Inject] private ActionFilter _actions;
        [Inject] private IUIManager _uiManager;
        
        private void Awake()
        {
            _context.Run();
        }

        private void Start()
        {
            _dataSaver.LoadData();
            _levelManager.Init();
            if(doLoadLevels)
                _levelManager.LoadLast();
            _input.IsEnabled = false;
            _actions.IsEnabled = false;
            _uiManager.Init();
        }

        private void OnDestroy()
        {
            _dataSaver.SaveData();
        }

    }
}