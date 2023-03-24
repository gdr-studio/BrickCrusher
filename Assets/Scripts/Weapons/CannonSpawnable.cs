using DG.Tweening;
using Merging;
using UnityEngine;
using Weapons.Shooting;

namespace Weapons
{
    public class CannonSpawnable : MonoBehaviour
    {
        public Cannon cannon;
        public ShotsLeftCounter shotsCounter; 
        public float NormalScale;
        public Ease ScaleEase;
        public Ease MovingEase;
        public float MoveTime = 0.5f;
        public float ScaleTime = 0.5f;
        public ParticleSystem spawnParticles;

        public MergingItemArea MergingArea { get; set; }
        public void Spawn()
        {
            SpawnScalingUp();
            shotsCounter.SetCount(cannon.Settings.shooting.MaxShots);
            shotsCounter.InitRotation();
        }

        public void SpawnScalingUp()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one * NormalScale, ScaleTime).SetEase(ScaleEase);
            spawnParticles.Play();
        }
        
        public void Delete()
        {
            var time = (transform.localScale.x) / NormalScale * ScaleTime;
            transform.DOScale(Vector3.zero, time).SetEase(ScaleEase).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        public void Move(Vector3 position)
        {
            transform.DOMove(position, MoveTime).SetEase(MovingEase);
        }
    }
}