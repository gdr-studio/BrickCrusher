using System.Collections;
using Data.Game;
using React;
using UnityEngine;

namespace Weapons.Shooting
{
    public class CannonShooter : MonoBehaviour
    {
 
        public ShotsLeftCounter shotsCounter;
        public BallShooter ballShooter;
        private ReactiveProperty<int> _leftShots;
        private Coroutine _shooting;
        private float _elapsedShootTime;
        private bool _didShoot;

        public int MaxShoots { get; private set; }
        public float FirePeriod { get; private set; }
        
        private ShootingSettings _settings;
        public ShootingSettings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                _leftShots = new ReactiveProperty<int>();
                _leftShots.Val = _settings.MaxShots;
                MaxShoots = _settings.MaxShots;
                GlobalData.ShotsLeft.Val = MaxShoots;
                shotsCounter.Init(_leftShots);
                FirePeriod = _settings.ShootDelay;
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
            while (_leftShots.Val > 0)
            {
                while (_elapsedShootTime < FirePeriod && _didShoot)
                {
                    _elapsedShootTime += Time.deltaTime;
                    yield return null;
                }
                yield return null;
                MinusShot();
                _elapsedShootTime = 0f;
                _didShoot = true;
                ballShooter.ShootForward();
            }
        }
        
        private void MinusShot()
        {
            _leftShots.Val--;
        }
        
            
    }
}