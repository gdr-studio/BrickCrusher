using Game.Sound;
using GameUI;
using Levels;
using PlayerInput;
using UnityEngine;
using Zenject;

namespace Main.Boot
{
    public class GameBoot : MonoBehaviour
    {
        public bool doLoadLevels;
        [SerializeField] private SceneContext _context;
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
            if(doLoadLevels)
                _levelManager.LoadLast();
            _input.IsEnabled = false;
            _actions.IsEnabled = false;
            _uiManager.Init();
        }
    }
}