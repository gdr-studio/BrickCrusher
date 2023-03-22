using System.Collections.Generic;
using Data.Game;
using UnityEngine;
using Weapons.Movement;
using Weapons.Shooting;

namespace Weapons
{
    public class CannonsController : MonoBehaviour, IWeapon
    {
        public CannonSettings settings;
        public WeaponOneDMover mover;
        public CannonsShooter shooter;
        public MoverLimitSetter limitSetter;
        public List<Cannon> cannons;
        
        public void Init()
        {
            GlobalData.CurrentWeapon = this;
            limitSetter.SetLimits();
            mover.Settings = settings.moving;
            
            List<ICannonShooter> cannonShooters = new List<ICannonShooter>();
            foreach (var cannon in cannons)
            {
                cannon.Init();   
                cannonShooters.Add(cannon.Shooter);
                cannon.Shooter.Settings = settings.shooting;
            }
            shooter.Shooters = cannonShooters;
            shooter.Settings = settings.shooting;
        }

   
        public void Kill()
        {
            shooter.StopShooting();
        }
        
        
        public void Grab()
        {
            shooter.StartShooting();   
        }

        public void Release()
        {
            shooter.StopShooting();
        }

        public void Move(Vector2 dir)
        {
            if (dir.x == 0)
                return;
            mover.Move(dir);
            foreach (var cannon in cannons)
            {
                cannon.Mover.Move(dir);
            }
        }

        
    }
}