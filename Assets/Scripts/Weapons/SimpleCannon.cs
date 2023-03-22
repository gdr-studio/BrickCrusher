using Weapons.Movement;
using Weapons.Shooting;

namespace Weapons
{
    public class SimpleCannon : Cannon
    {
        public CannonSettings Settings;
        
        public override void Init()
        {
            Mover = gameObject.GetComponent<IWeaponMover>();
            BallShooter = gameObject.GetComponent<IBallShooter>();
            // Shooter = gameObject.GetComponent<CannonShooter>();
            Shooter.Settings = Settings.shooting;
            BallShooter.Settings = Settings.shooting;
        }
        
  
    }
}