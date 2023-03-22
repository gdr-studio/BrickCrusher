using System;
using System.Collections.Generic;
using Helpers;
using ImageToVolume;
using UnityEngine;

namespace Statues
{
    public class BlockStatue : MonoBehaviour
    {
        public event Action OnAllBroken;
        public bool InitOnAwake = true;
        public List<VolumeElement> Puzzle = new List<VolumeElement>();

        private int _brokenCount;

        public int BrokenCount
        {
            get => _brokenCount;
            set
            {
                _brokenCount = value;
                if (_brokenCount >= Puzzle.Count)
                {
                    OnAllBroken?.Invoke();
                }
            }
        }
        
        
        
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

