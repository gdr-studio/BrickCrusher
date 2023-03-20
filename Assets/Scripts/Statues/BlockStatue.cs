using System.Collections.Generic;
using ImageToVolume;
using UnityEngine;

namespace Statues
{
    public class BlockStatue : MonoBehaviour
    {
        public bool InitOnAwake = true;
        public List<VolumeElement> Puzzle = new List<VolumeElement>();

        public int TotalCount => Puzzle.Count;
        public int BrokenCount { get; set; }
        
        private void OnValidate()
        {
            if(Puzzle != null)
                UpdateAll();
        }

        private void Awake()
        {
            if(InitOnAwake)
                UpdateAll();
        }

        public void UpdateAll()
        {
            foreach (var element in Puzzle)
            {
                element.UpdateTilingAndOffset();
            }
        }
        
        
    }
}

