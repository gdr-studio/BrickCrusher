using System.Collections;
using UnityEngine;

namespace GameCamera
{
    public class CameraMover : MonoBehaviour, ICameraMover
    {
        public CameraSettings _settings;
        public float LerpMoveSpeed = 1f;
        public float LerpRotSpeed = 1f;
        public float RotMoveSpeed = 1f;
        public float MoveSpeed = 1f;
        [SerializeField] private AnimationCurve _rotCurve;
        [SerializeField] private AnimationCurve _moveCurve;
        public Transform MoveAt;
        public Transform LookAt;
        
        private Vector3 LookAtPoint;
        
        private Coroutine _moving;
        private Coroutine _rotating;
        private Coroutine _lookingAt;

        private void OnEnable()
        {
            InitSettings();
        }

        public void InitSettings()
        {
            if (_settings == null)
                return;
            LerpMoveSpeed = _settings.LerpMoveSpeed;
            LerpRotSpeed = _settings.LerpRotSpeed;
            RotMoveSpeed = _settings.RotMoveSpeed;
            MoveSpeed = _settings.MoveSpeed;
        }

        public void SetPosition(Vector3 position )
        {
            transform.position = position;
        }

        public void SetLookAt(Vector3 position)
        {
            transform.rotation = Quaternion.LookRotation(position - transform.position);
            LookAtPoint = position;
        }
        
        public void StopMoving()
        {
            if (_moving != null)
                StopCoroutine(_moving);
            if(_rotating != null)
                StopCoroutine(_rotating);
        }

        public void MoveToTarget(Transform moveTarget)
        {
            if(_moving != null)
                StopCoroutine(_moving);
            _moving = StartCoroutine(ChangingPosition(moveTarget.position));
        }

        public void ChangeLookAt(Transform target)
        {
            if(_rotating != null)
                StopCoroutine(_rotating);
            _rotating = StartCoroutine(ChangingLookPos(target.position));
            if(_lookingAt != null)
                StopCoroutine(_lookingAt);
            _lookingAt = StartCoroutine(LookingAtPos());
        }

        public void StartFollow(Transform target)
        {
            if(_lookingAt != null)
                StopCoroutine(_lookingAt);
            if(_moving != null)
                StopCoroutine(_moving);
            MoveAt = target;
            _moving = StartCoroutine(FollowingLerp());
        }

        public void StartLookingAt(Transform target)
        {
            if(_lookingAt != null)
                StopCoroutine(_lookingAt);
            if(_rotating != null)
                StopCoroutine(_rotating);
            LookAt = target;
            _rotating = StartCoroutine(LookingAtLerp());
        }
        
        private IEnumerator ChangingPosition(Vector3 endPos)
        {
            var start = transform.position;
            var time = (endPos - start).magnitude / MoveSpeed;
            if (time == 0)
            {
                transform.position = endPos;
                yield break;
            }
            var elapsed = 0f;
            while (elapsed <= time)
            {
                var t = elapsed / time;
                transform.position = Vector3.Lerp(start, endPos, t);
                elapsed += Time.deltaTime * _moveCurve.Evaluate(t);
                yield return null;
            }
            transform.position = endPos;
        }
        
        private IEnumerator ChangingLookPos(Vector3 endPos)
        {
            var start = LookAtPoint;
            var time = (endPos - start).magnitude / RotMoveSpeed;
            var elapsed = 0f;
            while (elapsed <= time)
            {
                var t = elapsed / time;
                LookAtPoint = Vector3.Lerp(start, endPos, t);
                elapsed += Time.deltaTime * _rotCurve.Evaluate(t);
                yield return null;
            }
            LookAtPoint = endPos;
        }


        private IEnumerator FollowingLerp()
        {
            while (true)
            {
                transform.position = Vector3.Lerp(transform.position, MoveAt.position, LerpMoveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        
        
        private IEnumerator LookingAtLerp()
        {
            while (true)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(LookAt.position - transform.position), LerpRotSpeed * Time.deltaTime);
                yield return null;
            }
        }

        
        private IEnumerator LookingAtPos()
        {
            while (true)
            {
                transform.rotation = Quaternion.LookRotation(LookAtPoint - transform.position);
                yield return null;
            }
        }
    }
}