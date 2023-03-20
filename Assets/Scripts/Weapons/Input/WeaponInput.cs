using Data;
using PlayerInput;
using UnityEngine;
using Zenject;

namespace Weapons.Input
{
    [DefaultExecutionOrder(1000)]
    public class WeaponInput : MonoBehaviour
    {
        [Inject] private IInputManager _inputManager;
        [Inject] private Layers _layers;
        private IWeapon _current;
        private Camera _camera;

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (value && !_isEnabled)
                {
                    Activate();
                }
                if (!value && _isEnabled)
                {
                    Deactivate();
                }
                _isEnabled = value;
            }
        }

        public void SetTarget(IWeapon weapon)
        {
            _current = weapon;
            if(weapon != null)
                weapon.Grab();
        }
        
        private void Activate()
        {
            _camera = Camera.main;
            _inputManager.OnMove += OnMove;
            _inputManager.OnRelease += OnRelease;
        }
        
        private void Deactivate()
        {
            _inputManager.OnMove -= OnMove;  
            _inputManager.OnRelease -= OnRelease;
        }
        
        
        private void OnMove(Vector2 dir)
        {
            if (_current != null)
            {
                _current.Move(dir);
            }
        }

        private void OnRelease(Vector2 obj)
        {
            _current?.Release();
        }
        
        private void OnClick(Vector2 click)
        {
            var ray = _camera.ScreenPointToRay(click);
            if (Physics.Raycast(ray, out var hit, 100,_layers.DefaultMask))
            {
                // Debug.Log($"hit: {hit.collider.gameObject.name}");
                var weapon = hit.collider.gameObject.GetComponent<IWeapon>();
                _current = weapon;
                _current?.Grab();
                
            }
            else
            {
                Debug.Log("[WeaponInput] NO hit");
            }
        }
        

  
    }
}