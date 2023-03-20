using System;
using DG.Tweening;
using UnityEngine;

namespace LittleTricks
{
    public class TweenScaler : MonoBehaviour
    {
        public Transform target;
        [Header("Durations")] 
        public float toDownTime;
        public float toNormalTime;
        public float toUpTime;
        [Header("Scales")] 
        public float minScale;
        public float maxScale;
        public float normalScale;
        
        protected Sequence _scalingSeq;

        public void ScaleDownAndBack(Action onEnd)
        {
            _scalingSeq?.Kill();
            _scalingSeq = DOTween.Sequence();
            _scalingSeq.Append(target.DOScale(Vector3.one * minScale, toDownTime))
                .Append(target.DOScale(Vector3.one * normalScale, toNormalTime))
                .OnComplete(() => { onEnd?.Invoke(); });
        }

        public void ScaleUpAndBack(Action onEnd)
        {
            _scalingSeq?.Kill();
            _scalingSeq = DOTween.Sequence();
            _scalingSeq.Append(target.DOScale(Vector3.one * maxScale, toUpTime))
                .Append(target.DOScale(Vector3.one * normalScale, toNormalTime))
                .OnComplete(() => { onEnd?.Invoke(); });
        }

        public void ScaleFromTo(float from, float to, float time, Action onEnd)
        {
            _scalingSeq?.Kill();
            target.localScale = Vector3.one * from;
            target.DOScale(Vector3.one * to, time).SetEase(Ease.InCubic).OnComplete(() => { onEnd?.Invoke(); });
        }
        
    }
}