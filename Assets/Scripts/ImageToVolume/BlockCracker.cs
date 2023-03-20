using Statues.Cracking;
using UnityEngine;

namespace ImageToVolume
{
    public class BlockCracker : MonoBehaviour
    {
        private CrackTexture _crack;
        private bool _cracked;
        
        public void Crack(CrackTexture crack)
        {
            _cracked = true;
            _crack = crack;
            var parent = transform.parent;
            var scaleX = transform.localScale.x;
            var scaleZ = transform.localScale.z;
            while (parent != null)
            {
                scaleX *= parent.localScale.x;
                scaleZ *= parent.localScale.z;
                parent = parent.parent;
            }
            crack.ShowAt(transform.position, scaleX, scaleZ,-transform.forward);
            crack.Rotate(transform.rotation);
        }

        public void Hide()
        {
            if (_cracked)
            {
                _crack.Hide();
            }
        }
    }
}