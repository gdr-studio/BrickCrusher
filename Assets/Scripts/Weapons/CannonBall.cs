using Data;
using DG.Tweening;
using Statues;
using UnityEngine;

namespace Weapons
{
    public class CannonBall : MonoBehaviour
    {
        public Ease moveEase;
        public Layers layers;
        private ShootingSettings _settings;
        
        public void Shoot(Vector3 dir, ShootingSettings settings)
        {
            _settings = settings;
            var moveVec = dir * settings.MaxMoveDist;
            transform.DOMove(transform.position + moveVec, settings.MoveTime).SetEase(moveEase).OnComplete(() =>
            {
                Hide();
            });
        }

        private void HitOne(GameObject go)
        {
            var piece = go.GetComponent<IStatuePiece>();
            piece.Damage(new DamageArgs()
            {
                Amount = _settings.Damage
            });
        }

        private void HitRange()
        {
            var range = _settings.Range;
            var damage = new DamageArgs();
            damage.Amount = _settings.Damage;
            var results = Physics.OverlapBox(transform.position, new Vector3(range,range, 1f), Quaternion.identity,layers.PiecesMask);
            if (results.Length <= 0) 
                return;
            
            // first - activate them
            for (var i = 0; i < results.Length; i++)
            {
                var block = results[i];
                var piece = block.GetComponent<Collider>().gameObject.GetComponent<IStatuePiece>();
                piece.Damage(damage);
            }
            
            // then push all of them
            for (var i = 0; i < results.Length; i++)
            {
                var block = results[i];
                var piece = block.GetComponent<Collider>().gameObject.GetComponent<IStatuePiece>();
                piece.Push();
            }
        }

        public void Hide()
        {
            transform.DOKill();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            // Debug.Log($"on trigger with: {other.gameObject.name}, layer: {other.gameObject.layer},  target layer: {layers.PiecesLayer}");
            if (other.gameObject.layer == layers.PiecesLayer)
            {
                transform.DOKill();
                if (_settings.DamageOne)
                {
                    HitOne(other.attachedRigidbody.gameObject);
                }
                else
                {
                    HitRange();
                }
                Hide();
            }
        }
        
        
    }
}
