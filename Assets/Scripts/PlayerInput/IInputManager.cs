using System;
using UnityEngine;

namespace PlayerInput
{
    public interface IInputManager
    {
        public bool IsEnabled { get; set; }
        public event Action<Vector2> OnClick;
        public event Action<Vector2> OnRelease;
        public event Action<Vector2> OnMove;

    }
}