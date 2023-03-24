using System;
using UnityEngine;

namespace Weapons
{
    public interface IWeapon
    {
        public event Action OnGrabbed;
        void Grab();
        void Release();
        void Move(Vector2 dir);
        void Kill();
        Vector3 Position { get; }
    }
}