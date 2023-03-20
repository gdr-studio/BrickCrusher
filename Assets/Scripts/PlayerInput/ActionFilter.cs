using Data;
using Data.Game;
using Money;
using UnityEngine;
using Weapons.Input;
using Zenject;

namespace PlayerInput
{
    [DefaultExecutionOrder(-100)]
    public class ActionFilter : MonoBehaviour
    {
        [Inject] private IInputManager _inputManager;
        [Inject] private Layers _layers;
        [SerializeField] private float _castRad;
        [SerializeField] private Collector _collector;
        [SerializeField] private WeaponInput _weaponInput;
        
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
        
        private void Activate()
        {
            _weaponInput.IsEnabled = true;
            _inputManager.OnClick += OnClick;
            _camera = Camera.main;
        }

        private void Deactivate()
        {
            _weaponInput.IsEnabled = false;
            _inputManager.OnClick -= OnClick;
        }
        
        private void OnClick(Vector2 click)
        {
            var ray = _camera.ScreenPointToRay(click);
            if (Physics.SphereCast(ray,_castRad, out var hit, 100, _layers.DefaultMask))
            {
                if (TryGetCollectable(hit.collider.gameObject) == false)
                {
                    if (hit.collider.attachedRigidbody != null)
                    {
                        TryGetCollectable(hit.collider.attachedRigidbody.gameObject);
                    }
                }
            }
            else
            {
                _weaponInput.SetTarget(GlobalData.CurrentWeapon);
            }
        }

        private bool TryGetCollectable(GameObject go)
        {
            var collectable = go.GetComponent<ICollectable>();
            if (collectable == null)
            {
                _weaponInput.SetTarget(GlobalData.CurrentWeapon);
                return false;
            }
            _collector.Collect(collectable);
            return true;
        }
     
    }
}