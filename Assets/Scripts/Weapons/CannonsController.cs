// #define SET_WEAPONS_SAME
using System;
using System.Collections.Generic;
using Data.Game;
using UnityEngine;
using Weapons.Movement;
using Weapons.Shooting;


namespace Weapons
{
    public class CannonsController : MonoBehaviour, IWeapon
    {
        public event Action OnNoAmmo;
        public event Action OnGrabbed;

        public CannonSettings settings;
        public WeaponOneDMover mover;
        public CannonsShooter shooter;
        public MoverLimitSetter limitSetter;
        public List<Cannon> cannons;
        private int _outOfAmmoCannonsCount;
        
        public void Init()
        {
            GlobalData.CurrentWeapon = this;
            limitSetter.SetLimits();
            mover.Settings = settings.moving;
            #if SET_WEAPONS_SAME
            var cannonShooters = new List<IBallShooter>();
            foreach (var cannon in cannons)
            {
                cannon.Init();   
                cannonShooters.Add(cannon.BallShooter);
                cannon.BallShooter.Settings = settings.shooting;
            }
            shooter.Shooters = cannonShooters;
            shooter.Settings = settings.shooting;
            #else
            foreach (var cannon in cannons)
            {
                cannon.Init();
                cannon.Shooter.OnOutOfAmmo += OnOutOfAmmo;
            }
            #endif
        }

        private void OnOutOfAmmo()
        {
            _outOfAmmoCannonsCount++;
            if (_outOfAmmoCannonsCount == cannons.Count)
            {
                OnNoAmmo.Invoke();   
            }
        }

        public void Kill()
        {
            shooter.StopShooting();
            foreach (var cannon in cannons)
            {
                cannon.Shooter.StopShooting();
            }
        }

        public Vector3 Position => transform.position;


        public void Grab()
        {
#if SET_WEAPONS_SAME
            shooter.StartShooting();
#else
            foreach (var c in cannons)
            {
                c.Shooter.StartShooting();
            }
#endif
            OnGrabbed?.Invoke();
        }

        public void Release()
        {
#if SET_WEAPONS_SAME
            shooter.StopShooting();
#else
            foreach (var c in cannons)
            {
                c.Shooter.StopShooting();
            }
            #endif
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