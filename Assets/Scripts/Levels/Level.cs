using System;
using UnityEngine;

namespace Levels
{
    public abstract class Level : MonoBehaviour
    {
        public event Action OnStarted;
        public abstract void Init();
        public abstract void StartLevel();

        protected void RaiseOnStarted() => OnStarted?.Invoke();
    }
}