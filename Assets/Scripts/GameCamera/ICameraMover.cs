using UnityEngine;

namespace GameCamera
{
    public interface ICameraMover
    {
        public void MoveToTarget(Transform target);
        public void ChangeLookAt(Transform target);

        public void StartFollow(Transform target);
        public void StartLookingAt(Transform target);

        
        public void StopMoving();
        public void SetPosition(Vector3 position);
        public void SetLookAt(Vector3 position);
    }
}