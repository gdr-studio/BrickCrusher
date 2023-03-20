using UnityEngine;

namespace GameCamera
{
    [CreateAssetMenu(fileName = nameof(CameraSettings), menuName = "SO/" + nameof(CameraSettings))]
    public class CameraSettings : ScriptableObject
    {
        public float LerpMoveSpeed = 1f;
        public float LerpRotSpeed = 1f;
        public float RotMoveSpeed = 1f;
        public float MoveSpeed = 1f;
    }
}