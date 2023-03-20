using UnityEngine;

namespace Weapons
{
    public interface IWeapon
    {
        void Grab();
        void Release();
        void Move(Vector2 dir);
        void Kill();
    }
}