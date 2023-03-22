using Data;
using Statues.Cracking;
using UnityEngine;

namespace ImageToVolume
{
    public class BlockCracker : MonoBehaviour
    {
        public ColorSetter colorSetter;
        public MainGameConfig config;
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
            crack.ShowAt(new Vector3(0f, 0f, -transform.localScale.z / 2), transform, -Vector3.forward);
            crack.Rotate(transform.rotation);
            var startColor = Color.red * 0.75f;
            colorSetter.SetColor( startColor, Color.white * config.BlockDamageEndGray, config.BlockDamageColorTime);
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