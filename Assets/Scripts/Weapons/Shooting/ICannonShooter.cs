namespace Weapons.Shooting
{
    public interface ICannonShooter
    {
        void ShootForward();
        public ShootingSettings Settings { get; set; }
    }
}