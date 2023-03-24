using UnityEngine;
using Weapons.Movement;
using Weapons.Shooting;

namespace Weapons
{
    public abstract class Cannon : MonoBehaviour
    {
        public CannonSettings Settings;
        public abstract CannonName Name { get; }
        public abstract void Init();
        public IWeaponMover Mover;
        public IBallLauncher BallLauncher;
        public ICannonShooter Shooter;
    }
}