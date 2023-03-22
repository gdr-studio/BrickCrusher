using UnityEngine;
using Weapons.Movement;
using Weapons.Shooting;

namespace Weapons
{
    public abstract class Cannon : MonoBehaviour
    {
        public abstract void Init();
        public IWeaponMover Mover;
        public IBallShooter BallShooter;
        public CannonShooter Shooter;
    }
}