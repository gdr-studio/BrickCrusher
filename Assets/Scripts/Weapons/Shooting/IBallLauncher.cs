namespace Weapons.Shooting
{
    public interface IBallLauncher
    {
        void ShootForward();
        public ShootingSettings Settings { get; set; }
    }
}