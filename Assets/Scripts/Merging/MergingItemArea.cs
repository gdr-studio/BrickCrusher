using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Merging
{
    public class MergingItemArea : MonoBehaviour
    {
        public Image image;
        public MergingData currentData;
        public TextMeshProUGUI levelText;
        public CostBlock costBlock;
        public event Action<MergingItemArea> OnDataSet;
        
        private void OnEnable()
        {
            Init();
        }

        public void Init()
        {
            if (currentData != null)
            {
                UpdateByData();
            }
            else
            {
                SetEmpty();   
            }
        }
        
        public void SetData(MergingData data)
        {
            currentData = data;
            UpdateByData();
            OnDataSet?.Invoke(this);
            // Debug.Log($"area: {gameObject.name}, sprite: {data.sprite.name}");
        }

        public void TakeFrom()
        {
            SetEmpty();
            OnDataSet?.Invoke(this);
        }

        public bool IsEmpty() => currentData == null;
        
        private void UpdateByData()
        {
            image.enabled = true;
            image.sprite = currentData.sprite;
            levelText.enabled = true;
            levelText.text = $"{currentData.level}";   
        }

        private void SetEmpty()
        {
            image.sprite = null;
            image.enabled = false;
            currentData = null;
            levelText.enabled = false;
        }

        public void ShowCost(int cost)
        {
            costBlock.Show(cost);
        }

        public void HideCost()
        {
            costBlock.Hide();
        }

 
    }
}