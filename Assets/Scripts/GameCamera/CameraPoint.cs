using UnityEngine;
using Zenject;

namespace GameCamera
{
    public class CameraPoint : MonoBehaviour
    {
        public Transform Point;
        public Transform LookTarget;
        public bool AutoSetInEditor = true;
        
        [Inject] private ICameraMover _camera;

        public void SetToThisPoint()
        {
#if UNITY_EDITOR
            if (SetFromEditor())
                return;
#endif
            _camera.StopMoving();
            _camera.SetPosition(Point.position);
            _camera.SetLookAt( LookTarget.position);
        }

        public void MoveToThisPoint()
        {
#if UNITY_EDITOR
            if (SetFromEditor())
                return;
#endif
            _camera.ChangeLookAt(LookTarget);
            _camera.MoveToTarget(Point);
        }

        public void FollowThisPoint()
        {
#if UNITY_EDITOR
            if (SetFromEditor())
                return;
#endif
            _camera.StartFollow(Point);
            _camera.StartLookingAt(LookTarget);   
        }
        
        
        private bool SetFromEditor()
        {
            if (Application.isPlaying == false)
            {
                var cam = FindObjectOfType<CameraMover>().transform;
                cam.position = Point.position;
                cam.rotation = Quaternion.LookRotation(LookTarget.position - Point.position);
                return true;
            }

            return false;
        }
        
    }
}