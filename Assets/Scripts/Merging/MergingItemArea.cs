using System;
using DG.Tweening;
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
        public Color spawnedColor;
        [Space(20)] 
        [SerializeField] private float _punchScale = 1.2f;
        [SerializeField] private float _punchDur = 0.2f;
        
        public bool IsTaken { get; set; }
        public bool IsEmpty() => currentData == null;

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
            IsTaken = false;
            OnDataSet?.Invoke(this);
            // Debug.Log($"area: {gameObject.name}, sprite: {data.sprite.name}");
        }

        public void TakeFrom()
        {
            // SetEmpty();
            SetTaken();
            OnDataSet?.Invoke(this);
        }

        
        private void UpdateByData()
        {
            SetAvailable();
            image.enabled = true;
            image.sprite = currentData.sprite;
            levelText.enabled = true;
            levelText.text = $"{currentData.level}";   
        }

        public void SetEmpty()
        {
            SetAvailable();
            image.sprite = null;
            image.enabled = false;
            currentData = null;
            levelText.enabled = false;
            IsTaken = false;
        }

        public void ShowCost(int cost)
        {
            costBlock.Show(cost);
        }

        public void HideCost()
        {
            costBlock.Hide();
        }

        public void SetSpawned()
        {
            image.color = spawnedColor;
            IsTaken = true;
        }

        public void SetTaken()
        {
            image.color = spawnedColor;
            IsTaken = true;
        }

        private void SetAvailable()
        {
            image.color = Color.white;
        }
        
        public void SetDataBack()
        {
            UpdateByData();
            IsTaken = false;
            transform.DOPunchScale(_punchScale * Vector3.one, _punchDur);
        }
 
    }
}