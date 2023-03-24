using UnityEngine;
using Weapons.Movement;
using Weapons.Shooting;

namespace Weapons
{
    public class SimpleCannon : Cannon
    {
        [SerializeField] private CannonName _cannonName;


        public override CannonName Name => _cannonName;

        public override void Init()
        {
            Mover = gameObject.GetComponent<IWeaponMover>();
            BallLauncher = gameObject.GetComponent<IBallLauncher>();
            Shooter = gameObject.GetComponent<ICannonShooter>();
            
            Shooter.Settings = Settings.shooting;
            BallLauncher.Settings = Settings.shooting;
        }
        
  
    }
}