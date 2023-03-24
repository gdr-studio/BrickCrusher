using DG.Tweening;
using React;
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
        private int _prevVal;
        public void Init(ReactiveProperty<int> shotsLeft)
        {
            shotsLeft.SubOnChange(OnCountChange);
            transform.rotation = Quaternion.identity;
            SetCount(shotsLeft.Val);
        }

        private void OnCountChange(int val)
        {
            if (_scaling != null)
            {
                _scaling.Kill();
                SetCount(val);
            }
            _scaling = DOTween.Sequence();
            _scaleTarget.localScale = Vector3.one * _normalScale;
            _scaling.Append(
                    _scaleTarget.DOScale(Vector3.one * _smallScale, _scaleTime).SetEase(_scaleEase)
                        .OnComplete(() => { SetCount(val); })
                ).Append(_scaleTarget.DOScale(Vector3.one * _normalScale, _scaleTime).SetEase(_scaleEase))
                .OnComplete(() => { _scaling = null;});
        }

        public void SetCount(int count)
        {
            _prevVal = count;
            _text.text = $"{count}";
        }
    }
}