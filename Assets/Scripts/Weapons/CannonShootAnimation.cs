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
            target.localScale = settings.normalScale * 100f;
            _shootSeq?.Kill();
            _shootSeq = DOTween.Sequence();
            _shootSeq.Append(target.DOScale(settings.preScale * 100f, settings.preTime).OnComplete(() => { onShoot.Invoke(); }))
                .Append(target.DOScale(settings.afterScale * 100f, settings.afterTime)) 
                .Append(target.DOScale(settings.normalScale * 100f , settings.backTime));
        }

        public void StopAll()
        {
            target.DOKill();
        }

    }
}