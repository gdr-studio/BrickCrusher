using Data.Game;
using UnityEngine;
using Weapons.Movement;
using Weapons.Shooting;

namespace Weapons
{
    public class SimpleCannon : Cannon, IWeapon
    {
        public bool startShooting;
        public MoverLimitSetter limitSetter;
        public CannonSettings settings;

        private IWeaponMover _mover;
        private IWeaponShooter _shooter;

        public override void Init()
        {
            _mover = gameObject.GetComponent<IWeaponMover>();
            _shooter = gameObject.GetComponent<IWeaponShooter>();
            limitSetter.SetLimits();
            GlobalData.CurrentWeapon = this;
            _mover.Settings = settings.moving;
            _shooter.Settings = settings.shooting;
        }
        
        
        public override void Activate()
        {
        }
        
        public void Grab()
        {
            if (startShooting)
            {
                _shooter.StartShooting();
            }
        }

        public void Release()
        {
            _mover.Stop();
            _shooter.StopShooting();
        }

        public void Move(Vector2 dir)
        {
            _mover.Move(dir);
        }
        
        public override void Kill()
        {
            _mover.Stop();
            _shooter.StopShooting();   
        }

  
    }
}