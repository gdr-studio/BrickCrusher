using System;
using DG.Tweening;
using UnityEngine;

namespace Weapons
{
    public class CannonShootAnimation : MonoBehaviour
    {
        public Transform target;
        public CannonShootAnimationSettings settings;
        private Sequence _shootSeq;
        
        public void PlayShoot(Action onShoot)
        {
            target.localScale = settings.normalScale;
            _shootSeq?.Kill();
            _shootSeq = DOTween.Sequence();
            _shootSeq.Append(target.DOScale(settings.preScale, settings.preTime).OnComplete(() => { onShoot.Invoke(); }))
                .Append(target.DOScale(settings.afterScale, settings.afterTime)) 
                .Append(target.DOScale(settings.normalScale, settings.backTime));
        }

        public void StopAll()
        {
            target.DOKill();
        }

    }
}