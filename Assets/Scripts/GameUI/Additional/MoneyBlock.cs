using Data;
using DG.Tweening;
using Money;
using TMPro;
using UnityEngine;

namespace GameUI
{
    public class MoneyBlock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Transform _scaleTarget;
        [SerializeField] private float _smallScale;
        [SerializeField] private float _scaleTime;
        private Sequence _scaling;
        private int _targetCount;

        public void SetCount()
        {
            _text.text = $"{MoneyCounter.TotalMoney.Val}";
        }

        public void UpdateCount()
        {
            if (_scaling != null)
            {
                // _scaling.Kill();
                return;
            }
            _scaleTarget.localScale = Vector3.one;
            _scaling = DOTween.Sequence();
            _scaling.Append(
                _scaleTarget.DOScale(Vector3.one * _smallScale, _scaleTime).SetEase(Ease.InBack)
                    .OnComplete(() => { SetCount(); })
            ).Append(_scaleTarget.DOScale(Vector3.one * 1f, _scaleTime).SetEase(Ease.InBack))
                .OnComplete(() => { _scaling = null;});

        }

    }
}