namespace Weapons.Shooting
{
    public interface IWeaponShooter
    {
        ShootingSettings Settings { get; set; }
        void StartShooting();
        void StopShooting();
    }
}