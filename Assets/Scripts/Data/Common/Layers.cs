using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(Layers), menuName = "SO/" + nameof(Layers))]
    public class Layers : ScriptableObject
    {
        public LayerMask DefaultMask;
        public LayerMask PiecesMask;
        public int PiecesLayer;
        public int DroppedPiecesLayer;

    }
}