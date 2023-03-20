using System.Collections;
using Data.Game;
using Services.Parent;
using UnityEngine;
using Zenject;

namespace Weapons.Shooting
{
    public class CannonShooter : MonoBehaviour, IWeaponShooter
    {
        public int MaxShoots;
        public float FirePeriod;
        
        public CannonShootAnimation shootAnim;
        [SerializeField] private Transform _fromPoint;
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private ShotsLeftCounter _shotsCounter;

        [Inject] private DiContainer _container;
        [Inject] private IParentService _parentService;
        [Inject] private CannonBallRepository _ballRepository;
        
        private int _leftShots;
        private Coroutine _shooting;
        private float _elapsedShootTime;
        private bool _didShoot;
        private CannonBall _prefab;

        private ShootingSettings _settings;

        public ShootingSettings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                _leftShots = _settings.MaxShots;
                MaxShoots = _settings.MaxShots;
                GlobalData.ShotsLeft.Val = MaxShoots;
                _shotsCounter.Init();
                FirePeriod = _settings.ShootDelay;
                _prefab = _ballRepository.GetPrefab(_settings.cannonBall);
            }
        }
        
        
        public void StartShooting()
        {
            if(_shooting != null)
                StopCoroutine(_shooting);
            _shooting = StartCoroutine(Shooting());
        }

        public void StopShooting()
        {
            if(_shooting != null)
                StopCoroutine(_shooting);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator Shooting()
        {
            while (_leftShots > 0)
            {
                while (_elapsedShootTime < FirePeriod && _didShoot)
                {
                    _elapsedShootTime += Time.deltaTime;
                    yield return null;
                }
                yield return null;
                MinusShot();
                _elapsedShootTime = 0f;
                shootAnim.PlayShoot(ShootBall);
                _didShoot = true;
            }
        }

        private void ShootBall()
        {
            var ballInstance = _container.InstantiatePrefabForComponent<CannonBall>(_prefab, _parentService.DefaultParent);
            ballInstance.transform.position = _fromPoint.transform.position;
            ballInstance.transform.rotation = _fromPoint.transform.rotation;
            ballInstance.Shoot(_fromPoint.up, _settings);
            _particle.Play();   
        }
        
        private void MinusShot()
        {
            _leftShots--;
            GlobalData.ShotsLeft.Val = _leftShots;
        }

        public void OrientFor(Transform target)
        {
            _fromPoint.rotation = target.rotation;
        }
        
    }
}