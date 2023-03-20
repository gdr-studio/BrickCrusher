using System;
using DG.Tweening;
using UnityEngine;

namespace LittleTricks
{
    public class TweenMoverTwoPoint : MoverBase
    {
        public Transform Target;
        public Ease Ease;
        public float MoveTime;
        public float DownDelay;
        public Transform p1;
        public Transform p2;
        private Sequence _moveSequence;

        public void MoveFrontAndBack(Action onP2, Action onBack)
        {
            _moveSequence?.Kill();
            _moveSequence = DOTween.Sequence();
            _moveSequence.Append(Target.DOMove(p2.position, MoveTime).SetEase(Ease)
                    .OnComplete(() =>
                    {
                        onP2?.Invoke();
                    }));
            _moveSequence.Append(Target.DOMove(p1.position, MoveTime).SetEase(Ease)
                    .OnComplete(() =>
                    {
                        onBack?.Invoke();
                    })
                    .SetDelay(DownDelay) );
        }

        public override void Stop()
        {
            _moveSequence?.Kill();
        }
    }
}