using System;
using System.Collections;
using UnityEngine;

namespace PlayerInput
{
    public class PlayerInputManager : MonoBehaviour, IInputManager
    {
        private Coroutine _inputTaking;
        
        public event Action<Vector2> OnClick;
        public event Action<Vector2> OnRelease;
        public event Action<Vector2> OnMove;

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                if(_inputTaking != null)
                    StopCoroutine(_inputTaking);
                if (value)
                {
                    _inputTaking = StartCoroutine(InputTaking());
                }
            }
        }

        private IEnumerator InputTaking()
        {
            var oldPos = Vector2.zero;
            var newPos = Vector2.zero;
            oldPos = newPos = Input.mousePosition;    
            
            while (true)
            {
                newPos = Input.mousePosition;    
                if (Input.GetMouseButtonDown(0))
                {
                    OnClick?.Invoke(Input.mousePosition);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    OnRelease?.Invoke(Input.mousePosition);
                }

                if (Input.GetMouseButton(0))
                {
                    newPos = Input.mousePosition;
                    var diff = newPos - oldPos;
                    OnMove?.Invoke(diff);
                }
                oldPos = newPos;
                yield return null;
            }
        }
        
        
    }
}