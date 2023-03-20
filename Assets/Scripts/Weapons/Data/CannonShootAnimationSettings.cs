using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = nameof(CannonShootAnimationSettings), menuName ="SO/" + nameof(CannonShootAnimationSettings))]
    public class CannonShootAnimationSettings : ScriptableObject
    {
        public Vector3 normalScale;
        public Vector3 preScale;
        public Vector3 afterScale;
        public float preTime;
        public float afterTime;
        public float backTime;   
    }
}