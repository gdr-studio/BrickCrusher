using UnityEngine;

namespace Weapons
{
    public abstract class Cannon : MonoBehaviour
    {
        public abstract void Init();
        public abstract void Kill();
        public abstract void Activate();
    }
}