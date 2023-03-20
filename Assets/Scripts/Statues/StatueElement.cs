using System.Collections.Generic;
using Data;
using DG.Tweening;
using ImageToVolume;
using Statues.Cracking;
using UnityEngine;
using Zenject;

namespace Statues
{
    public class StatueElement : VolumeElement, IStatuePiece
    {
        public StatueElementSettings settings;
        public BlockStatue statue;
        public BreakablePiece piece;
        public BlockCracker cracker;

        [Inject] private CrackTexturePool _crackTexturePool;
        [Inject] private MainGameConfig _config;
        private bool _cracked;
        private int _health;
        private Sequence _moveSeq;
        private Vector3 localStartPos;
        public bool IsBroken { get; set; }
        
        private void Start()
        {
            _health = settings.Health;
            localStartPos = transform.localPosition;
        }

        public override void SetSubdivided(List<ElementSub> subs, int subSize)
        {
            base.SetSubdivided(subs, subSize);
            piece.coll.enabled = false;
        }

        public void Damage(DamageArgs args)
        {
            _health -= args.Amount;
            if (_health <= 0)
            {
                Break();
                return;
            }
            if (_cracked)
                return;
            _cracked = true;
            PlayDamagedAnim();
        }

        private void SetCrack()
        {
            if (isSubdivided)
            {
                foreach (var sub in subdivider.spawnedParts)
                {
                    var texture = _crackTexturePool.GetItem();
                    sub.cracker.Crack(texture);
                }
            }
            else
            {
                var texture = _crackTexturePool.GetItem();
                cracker.Crack(texture);
            }
        }

        public void Break()
        {
            _moveSeq?.Kill();
            piece.HideSelf();
            renderer.enabled = false;
            IsBroken = true;
            cracker.Hide();
            if (isSubdivided)
            {
                subdivider.DropAll();
            }
            else
            {
                subdivider.SpawnAndDrop();
            }
            statue.BrokenCount++;
            subdivider.PushRandom();

        }

        public void Push()
        {
        }
        
        public void NeighboursOn(bool on )
        {
            foreach (var index in neighbourIndices)
            {
                statue.Puzzle[index].renderer.enabled = on;
            }
        }

        public void PlayDamagedAnim()
        {
            _moveSeq?.Kill();
            _moveSeq = DOTween.Sequence();
            var endPos = localStartPos;
            endPos.z += _config.ElementShakeMagn;
            _moveSeq.Append(transform.DOLocalMove(endPos, _config.ElementShakeDur))
                .Append(transform.DOLocalMove(localStartPos, _config.ElementShakeDur).OnComplete(SetCrack));
        }
        
    }
    
}