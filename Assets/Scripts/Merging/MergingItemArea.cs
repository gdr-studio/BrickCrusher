﻿using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Merging
{
    public class MergingItemArea : MonoBehaviour
    {
        public ImageData mainImage;
        public ImageData backGround;
        public MergingData currentData;
        public TextMeshProUGUI levelText;
        public CostBlock costBlock;
        [Space(20)] 
        [SerializeField] private float _punchScaleBuy = 0.2f;
        [SerializeField] private float _punchScaleReturn = 0.2f;
        [SerializeField] private float _punchScaleMerge = 0.2f;
        [SerializeField] private float _punchDur = 0.2f;
        [Space(20)] 
        [SerializeField] private float _highlightScale = 0.2f;
        [SerializeField] private float _highlightDur = 0.2f;
        private Sequence _highlightSeq;

        [System.Serializable]
        public class ImageData
        {
            public Image image;
            public Color activeColor;
            public Color passiveColor;

            public void SetActive()
            {
                image.color = activeColor;
            }

            public void SetPassive()
            {
                image.color = passiveColor;
            }

            public void SetImageActive(Sprite sprite)
            {
                image.enabled = true;
                image.sprite = sprite;
                image.color = activeColor;
            }

            public void HideAndClear()
            {
                image.sprite = null;
                image.enabled = false;
                image.color = activeColor;
            }
        }
        
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
            backGround.SetActive();
            mainImage.SetImageActive(currentData.sprite);
            levelText.enabled = true;
            levelText.text = $"{currentData.level}";   
        }

        public void SetEmpty()
        {
            backGround.SetPassive();
            SetAvailable();
            mainImage.HideAndClear();
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
            mainImage.SetPassive();
            IsTaken = true;
        }

        public void SetTaken()
        {
            backGround.SetPassive();
            mainImage.SetPassive();
            IsTaken = true;
        }

        private void SetAvailable()
        {
            mainImage.SetActive();
        }
        
        public void SetDataBack()
        {
            UpdateByData();
            IsTaken = false;
        }

        public void PlayMergeEffect()
        {
            transform.DOPunchScale(_punchScaleMerge * Vector3.one, _punchDur);
        }

        public void PlayReturnEffect()
        {
            transform.DOPunchScale(_punchScaleReturn * Vector3.one, _punchDur);
        }

        public void PlayBuyEffect()
        {
            transform.DOPunchScale(_punchScaleBuy * Vector3.one, _punchDur);
        }

        public void Highlight()
        {
            _highlightSeq = DOTween.Sequence();
            _highlightSeq.Append(transform.DOScale(_highlightScale * Vector3.one, _highlightDur))
                .Append(transform.DOScale(Vector3.one, _highlightDur))
                .SetLoops(-1);
        }

        public void StopHighlight()
        {
            _highlightSeq.Kill();
            transform.localScale = Vector3.one;
        }

        public Vector2 CenterPosition => (Vector2)mainImage.image.transform.position +
                                         mainImage.image.gameObject.GetComponent<RectTransform>().sizeDelta / 4f;
    }
}