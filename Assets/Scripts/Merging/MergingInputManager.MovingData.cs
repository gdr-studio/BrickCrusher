using UnityEngine;
using UnityEngine.UI;

namespace Merging
{
    public partial class MergingManager
    {
        private class MovingData
        {
            public Transform movable;
            public Image image;
            public MergingItemArea fromArea;
            public MergingData data;

            private bool _isSpawned;
            public bool IsSpawned
            {
                get => _isSpawned;
                set
                {
                    if (_isSpawned == value)
                        return;
                    _isSpawned = value;
                    if (value)
                    {
                        image.enabled = false;
                    }
                    else
                    {
                        image.enabled = true;
                    }
                }
            }

            public void Show(MergingData otherData)
            {
                image.enabled = true;
                image.sprite = otherData.sprite;
                data = otherData;
            }
            
            
            public void HideAndClear()
            {
                _isSpawned = false;
                image.enabled = false;
                image.sprite = null;
                fromArea = null;
                data = null;
            }
        }
    }
}