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
        [SerializeField] private SceneContext _context;
        [Inject] private ILevelManager _levelManager;
        [Inject] private ISoundManager _soundManager;
        [Inject] private IInputManager _input;
        [Inject] private IUIManager _uiManager;
        [Inject] private ActionFilter _actions;
        
        private void Awake()
        {
            _context.Run();
        }

        private void Start()
        {
            _levelManager.LoadLast();
            _input.IsEnabled = true;
            _actions.IsEnabled = true;
            _uiManager.ShowProgress();
        }
    }
}