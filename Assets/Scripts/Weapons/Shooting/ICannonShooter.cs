using System;

namespace Weapons.Shooting
{
    public interface ICannonShooter
    {
        public event Action OnOutOfAmmo;
        ShootingSettings Settings { get; set; }
        void StartShooting();
        void StopShooting();

    }
}