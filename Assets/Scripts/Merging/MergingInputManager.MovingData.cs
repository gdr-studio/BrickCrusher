using UnityEngine;
using UnityEngine.UI;

namespace Merging
{
    public partial class MergingInputManager
    {
        private class MovingData
        {
            public Transform movable;
            public Image image;
            public MergingItemArea fromArea;
            public MergingData data;

            public void Show(MergingData otherData)
            {
                image.enabled = true;
                image.sprite = otherData.sprite;
                data = otherData;
            }
            public void HideAndClear()
            {
                image.enabled = false;
                image.sprite = null;
                fromArea = null;
                data = null;
            }
        }
    }
}