namespace Weapons.Shooting
{
    public interface IBallShooter
    {
        void ShootForward();
        public ShootingSettings Settings { get; set; }
    }
}