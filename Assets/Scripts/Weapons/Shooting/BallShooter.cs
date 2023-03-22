using Services.Parent;
using UnityEngine;
using Zenject;

namespace Weapons.Shooting
{
    public class BallShooter : MonoBehaviour, IBallShooter
    {
        [SerializeField] private CannonShootAnimation _shootAnim;
        [SerializeField] private Transform _fromPoint;
        [SerializeField] private ParticleSystem _particle;


        [Inject] private DiContainer _container;
        [Inject] private IParentService _parentService;
        [Inject] private CannonBallRepository _ballRepository;
        private CannonBall _prefab;

        private ShootingSettings _settings;

        public ShootingSettings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                _prefab = _ballRepository.GetPrefab(_settings.cannonBall);
            }
        }
        
        public void ShootForward()
        {
            _shootAnim.PlayShoot(SpawnBallAndShoot);
        }

        private void SpawnBallAndShoot()
        {
            // Debug.Log("SHOOTING BALL");
            // try
            // {
            //
            // }
            // catch (System.Exception ex)
            // {
            //     Debug.Log($"excption: {ex.Message}");
            // }
            var ballInstance =
                _container.InstantiatePrefabForComponent<CannonBall>(_prefab, _parentService.DefaultParent);
            ballInstance.transform.position = _fromPoint.transform.position;
            ballInstance.transform.rotation = _fromPoint.transform.rotation;
            ballInstance.Shoot(_fromPoint.up, _settings);
            _particle.Play();
        }
        
        public void OrientFor(Transform target)
        {
            _fromPoint.rotation = target.rotation;
        }
        
    }
}