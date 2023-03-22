using Data.Game;
using LevelBorders;
using PlayerInput;
using Services.Parent;
using Statues;
using UnityEngine;
using Weapons;
using Zenject;

namespace Levels.Game
{
    public class BlocksLevel : Level
    {
        public Cannon cannon;
        public BlockStatue statue;
        [Header("components")] 
        [SerializeField] private Borders _borders;
        [SerializeField] private StatueName _statueName;
        [SerializeField] private Transform _statueSpawn;
        [SerializeField] private WeaponsSpawner _spawner;

        [Inject] private DiContainer _container;
        [Inject] private IParentService _parentService;
        [Inject] private IStatueRepository _staturRepo;
        [Inject] private IInputManager _inputManager;
        [Inject] private ActionFilter _actions;
        [Inject] private CannonRepository _cannonRepo;
        
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

        private void EnableInput()
        {
            _inputManager.IsEnabled = true;
            _actions.IsEnabled = true;   
        }

        private void SpawnStatue()
        {
            var prefab = _staturRepo.GetPrefab(_statueName);
            var instance = _container.InstantiatePrefabForComponent<BlockStatue>(prefab, transform);
            instance.transform.position = _statueSpawn.position;
            instance.transform.rotation = _statueSpawn.rotation;
            instance.transform.localScale = _statueSpawn.localScale;
        }

  
    }
}