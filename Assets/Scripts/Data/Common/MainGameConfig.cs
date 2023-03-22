using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(MainGameConfig), menuName = "SO/" + nameof(MainGameConfig))]
    public class MainGameConfig : ScriptableObject
    {
        [Header("Merging")] 
        public float BlockDamageColorTime = 0.4f;
        public float BlockDamageEndGray = 0.7f;
        [Header("Merging")] 
        public int FirstLevelCannonCost = 10;        
        [Header("Cannons")]
        public float CannonMoveSpeed = 50;
        [Header("StatueElements")]
        public float ElementShakeMagnMin = 0.1f;
        public float ElementShakeMagnMax = 0.2f;
        public float ElementNeighbourShakeMagnMin = 0.1f;
        public float ElementNeighbourShakeMagnMax = 0.2f;

        public float ElementShakeDur = 0.2f;
        [Space(5)]
        public float ElementDropPushForce;
        public float ElementDownForceAdd;

    }
}