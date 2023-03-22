using UnityEngine;
using Weapons;

namespace Merging
{
    [CreateAssetMenu(fileName = nameof(MergingData), menuName = "SO/" + nameof(MergingData))]
    public class MergingData : ScriptableObject
    {
        public Sprite sprite;
        public CannonName cannonName;
        public int level;
    }
}