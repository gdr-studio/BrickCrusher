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
        [SerializeField] private CannonName _cannonName;
        [SerializeField] private Transform _statueSpawn;
        [SerializeField] private Transform _cannonSpawn;
        

        [Inject] private DiContainer _container;
        [Inject] private IParentService _parentService;
        [Inject] private IStatueRepository _staturRepo;
        [Inject] private IInputManager _inputManager;
        [Inject] private CannonRepository _cannonRepo;
        
        public override void Init()
        {
            _borders.Init();
            _parentService.DefaultParent = transform;
            SpawnStatue();
            SpawnCannon();
            _inputManager.IsEnabled = true;
        }

        public override void StartLevel()
        {
            _inputManager.IsEnabled = true;
   
        }

        private void SpawnStatue()
        {
            var prefab = _staturRepo.GetPrefab(_statueName);
            var instance = _container.InstantiatePrefabForComponent<BlockStatue>(prefab, transform);
            instance.transform.position = _statueSpawn.position;
            instance.transform.rotation = _statueSpawn.rotation;
            instance.transform.localScale = _statueSpawn.localScale;
        }

        private void SpawnCannon()
        {
            var prefab = _cannonRepo.GetPrefab(_cannonName);
            var instance = _container.InstantiatePrefabForComponent<Cannon>(prefab, transform);
            cannon = instance;
            instance.transform.position = _cannonSpawn.position;
            instance.transform.rotation = _cannonSpawn.rotation;
            instance.Init();
        }
    }
}