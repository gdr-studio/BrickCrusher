using Data.Game;
using GameUI;
using Helpers;
using LevelBorders;
using PlayerInput;
using Services.Parent;
using Statues;
using UnityEngine;
using Zenject;

namespace Levels.Game
{
    public class BlocksLevel : Level
    {
        public BlockStatue statue;
        [Header("components")] 
        [SerializeField] private Borders _borders;
        [SerializeField] private StatueName _statueName;
        [SerializeField] private Transform _statueSpawn;
        [SerializeField] private WeaponsSpawner _spawner;

        [Inject] private DiContainer _container;
        [Inject] private IParentService _parentService;
        [Inject] private IStatueRepository _statueRepo;
        [Inject] private IInputManager _inputManager;
        [Inject] private IUIManager _uiManager;
        [Inject] private ActionFilter _actions;
        
        public override void Init()
        {
            GlobalData.CurrentLevel = this;
            _borders.Init();
            _parentService.DefaultParent = transform;
            SpawnStatue();
            _inputManager.IsEnabled = true;
        }

        public override void StartLevel()
        {
            _spawner.SpawnGuns(EnableInput);
        }
        
        
        public void Win()
        {
            StopAll();
            _uiManager.ShowWin();
        }

        public void Fail()
        {   
            StopAll();
            _uiManager.ShowFail();
        }
        
        public void Restart()
        {
               
        }

        private void EnableInput()
        {
            _inputManager.IsEnabled = true;
            _actions.IsEnabled = true;   
        }

        private void StopAll()
        {
            _inputManager.IsEnabled = false;
            GlobalData.CurrentWeapon.Kill();
            GlobalData.CurrentWeapon = null;
        }
        
        private void SpawnStatue()
        {
            var prefab = _statueRepo.GetPrefab(_statueName);
            var instance = _container.InstantiatePrefabForComponent<BlockStatue>(prefab, transform);
            instance.transform.position = _statueSpawn.position;
            instance.transform.rotation = _statueSpawn.rotation;
            instance.transform.localScale = _statueSpawn.localScale;
            statue = instance;
            statue.OnAllBroken += OnBroken;
        }

        private void OnBroken()
        {
            Win();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Win();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                Fail();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }
    }
}