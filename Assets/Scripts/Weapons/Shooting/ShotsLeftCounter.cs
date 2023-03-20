using System;
using Data.Game;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Weapons.Shooting
{
    public class ShotsLeftCounter : MonoBehaviour
    {
        [SerializeField] private Transform _scaleTarget;
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private float _smallScale;
        [SerializeField] private float _normalScale;
        [SerializeField] private float _scaleTime;
        [SerializeField] private Ease _scaleEase;
        private Sequence _scaling;
        

        public void Init()
        {
            GlobalData.ShotsLeft.SubOnChange( OnCountChange );   
            transform.rotation = Quaternion.identity;
            SetCount();
        }

        private void OnDisable()
        {
            GlobalData.ShotsLeft.UnsubOnChange( OnCountChange );
        }

        private void OnCountChange(float val)
        {
            if (_scaling != null)
                return;
            _scaling = DOTween.Sequence();
            _scaleTarget.localScale = Vector3.one * _normalScale;
            _scaling.Append(
                    _scaleTarget.DOScale(Vector3.one * _smallScale, _scaleTime).SetEase(_scaleEase)
                        .OnComplete(() => { SetCount(); })
                ).Append(_scaleTarget.DOScale(Vector3.one * _normalScale, _scaleTime).SetEase(_scaleEase))
                .OnComplete(() => { _scaling = null;});
        }

        private void SetCount()
        {
            _text.text = $"{GlobalData.ShotsLeft.Val}";
        }
    }
}