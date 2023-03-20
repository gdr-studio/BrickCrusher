using UnityEngine;

namespace Weapons.Movement
{
    public interface IWeaponMover
    {
        MovingSettings Settings { get; set; }
        void Move(Vector2 dir);
        void Stop();
    }
}