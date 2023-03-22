using UnityEngine;

namespace Weapons.Movement
{
    public interface IWeaponMover
    {
        void Move(Vector2 dir);
        void Stop();
    }
}