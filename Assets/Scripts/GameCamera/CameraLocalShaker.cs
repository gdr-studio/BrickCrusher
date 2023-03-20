using System;
using System.Collections;
using UnityEngine;

namespace GameCamera
{
    public class CameraLocalShaker : MonoBehaviour
    {
        public float Duration;
        public float Magnitude;
        public Transform Target;
        private Coroutine _shaking;

        private void OnValidate()
        {
            Target.localPosition = Vector3.zero;
            Target.localRotation = Quaternion.identity;
        }

        private void Start()
        {
            Target.localPosition = Vector3.zero;
            Target.localRotation = Quaternion.identity;
        }

        public void DefaultShake()
        {
            Shake(Duration, Magnitude);
        }   

        public void Shake(float duration, float magnitude)
        {
            StopShaking();
            _shaking = StartCoroutine(Shaking(duration, magnitude));
        }

        public void StopShaking()
        {
            if(_shaking != null)
                StopCoroutine(_shaking);
            Target.localPosition = Vector3.zero;
            Target.localRotation = Quaternion.identity;
        }
        
        private IEnumerator Shaking(float duration, float magn)
        {
            var elapsed = 0f;
            while (elapsed < duration)
            {
                var pos = UnityEngine.Random.onUnitSphere * magn;
                Target.localPosition = pos;
                elapsed += Time.deltaTime;
                yield return null;
            }

            Target.localPosition = Vector3.zero;
        }

    }
}