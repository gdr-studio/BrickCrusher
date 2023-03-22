using Weapons.Movement;
using Weapons.Shooting;

namespace Weapons
{
    public class SimpleCannon : Cannon
    {

        public override void Init()
        {
            Mover = gameObject.GetComponent<IWeaponMover>();
            Shooter = gameObject.GetComponent<ICannonShooter>();
        }
        
  
    }
}