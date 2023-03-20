using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(MainGameConfig), menuName = "SO/" + nameof(MainGameConfig))]
    public class MainGameConfig : ScriptableObject
    {
        [Header("Cannons")]
        public float CannonMoveSpeed = 50;
        [Header("StatueElements")]
        public float ElementShakeMagn = 0.1f;
        public float ElementShakeDur = 0.2f;
        [Space(5)]
        public float ElementDropPushForce;
        public float ElementDownForceAdd;

    }
}