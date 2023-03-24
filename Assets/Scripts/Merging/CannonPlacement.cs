using System;
using UnityEngine;
using Weapons;

namespace Merging
{
    public class CannonPlacement : MonoBehaviour
    {
        public float rotSpeed;
        public CannonSpawnable spawnable;
        private bool _doRotate;
        
        public void Show()
        {
            transform.localEulerAngles = Vector3.zero;
            _doRotate = true;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _doRotate = false;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_doRotate == false)
                return;
            var eulers = transform.localEulerAngles;
            eulers.y += rotSpeed * Time.deltaTime;
            transform.localEulerAngles = eulers;
        }
    }
}