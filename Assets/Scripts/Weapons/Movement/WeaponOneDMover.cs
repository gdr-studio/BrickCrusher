using Data;
using UnityEngine;

namespace Weapons.Movement
{
    public class WeaponOneDMover : MonoBehaviour,IWeaponMover 
    {
        public Transform moveTarget;
        public float Limit;
        private Camera _camera;

        private void OnEnable()
        {
            _camera = Camera.main;
        }

        public MovingSettings Settings { get; set; }

        public void Move(Vector2 dir)
        {
            var pos = moveTarget.position;
            if (dir.x == 0)
                return;
            pos.x += Mathf.Sign(dir.x) * Settings.MoveSpeed * Time.deltaTime;    
            var relativeX = (pos.x - _camera.transform.position.x);
            if (relativeX >= Limit
                || relativeX <= -Limit)
            {
                return;
            }
      
            moveTarget.position = pos;
        }

        public void Stop()
        {
            
        }
    }
}