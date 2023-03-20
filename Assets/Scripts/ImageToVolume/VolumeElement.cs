using System.Collections.Generic;
using UnityEngine;

namespace ImageToVolume
{
    public class VolumeElement : MonoBehaviour
    {
        public Renderer renderer;
        public ColorSetter colorSetter;
        public ElementSubdivider subdivider;
        [Space(10)]
        public float sizeX = 1;
        public float sizeY = 1;
        [Space(10)]
        public int X;
        public int Y;
        public List<int> neighbourIndices;
        [Space(10)]
        public bool isSubdivided = false;
        
        public void UpdateTilingAndOffset()
        {
            colorSetter.UpdateTilingAndOffset();
            if (isSubdivided)
            {
                foreach (var sub in subdivider.spawnedParts)
                {
                    sub.colorSetter.UpdateTilingAndOffset();
                }
            }
        }
        
        public virtual void SetSubdivided(List<ElementSub> subs, int subSize)
        {
            subdivider.spawnedParts = subs;
            isSubdivided = true;
            renderer.enabled = false;
        }

    }
}