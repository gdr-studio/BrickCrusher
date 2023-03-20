#if UNITY_EDITOR
using System.Collections.Generic;
using ImageToVolume;
using LittleTricks;
using UnityEditor;
using UnityEngine;

namespace Statues
{
    public class StatueConstructor : MonoBehaviour
    {
        public ImageVolumizer volumizer;
        public int blocksPerBigPiece;
        [Space(10)]
        public List<GameObject> blocks;
        public List<BreakablePiece> spawnedSmallPieces;

        public void Spawn()
        {
            Clear();
            blocks = volumizer.spawnedBlocks;
        }

        public void Clear()
        {
            Destroyer.ClearGOList(spawnedSmallPieces);
        }
        
        
    }
    
}
#endif