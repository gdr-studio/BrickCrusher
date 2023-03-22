using System.Collections.Generic;
using UnityEngine;

namespace Weapons.Movement
{
    public class CannonMover : MonoBehaviour, IWeaponMover
    {
        [SerializeField] private List<Transform> _wheels;
        [SerializeField] private float _wheelsTurnSpeed = 100;
        
        public void Move(Vector2 dir)
        {
            var rotDir = -Mathf.Sign(dir.x);
            for (int i = 0; i < _wheels.Count; i++)
            {
                var wheel = _wheels[i];
                var eulers = wheel.localEulerAngles;
                eulers.z += rotDir * _wheelsTurnSpeed * Time.deltaTime;
                wheel.localEulerAngles = eulers;
            }
        }

        public void Stop()
        {
        }
    }
}