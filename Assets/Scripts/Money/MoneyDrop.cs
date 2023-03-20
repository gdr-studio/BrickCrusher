using System.Collections;
using LittleTricks;
using Services.Pool;
using UnityEngine;

namespace Money
{
    public class MoneyDrop : MonoBehaviour, IPooledObject<MoneyDrop>, ICollectable
    {
        public int amount = 1;
        public float autoCollectDelay = 0.5f;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Collider _coll;
        [SerializeField] private TweenScaler _scaler;
        [SerializeField] private ParticleSystem _particles;
        [SerializeField] private float _pushUpForce = 4f;
        private IPool<MoneyDrop> _pool;
        private Coroutine _delayedCollect;
        
        public void Init(IPool<MoneyDrop> pool)
        {
            _pool = pool;
        }

        public MoneyDrop GetObject() => this;
        

        public void HideToCollect()
        {
            gameObject.SetActive(false);   
        }

        public void DropOut(Vector3 position)
        {
            transform.position = position;
            transform.rotation = Quaternion.Euler(UnityEngine.Random.onUnitSphere * 180f);
            gameObject.SetActive(true);
            _coll.enabled = true;
            _rb.isKinematic = false;
            _rb.AddForce(_pushUpForce * Vector3.up, ForceMode.VelocityChange);
            StopDelayedCollection();
            _delayedCollect = StartCoroutine(DelayedCollect());
        }

        private void StopDelayedCollection()
        {
            if(_delayedCollect != null)
                StopCoroutine(_delayedCollect);
        }

        private IEnumerator DelayedCollect()
        {
            yield return new WaitForSeconds(autoCollectDelay);
            Collect();
        }
        
        public void Collect()
        {
            StopDelayedCollection();
            
            _particles.transform.parent = transform.parent;
            _particles.transform.position = transform.position;

            
            _scaler.ScaleDownAndBack(() =>
            {
                gameObject.SetActive(false);
                _rb.isKinematic = true;
                _coll.enabled = false;
                _particles.Play();
                _pool.Return(this);
                MoneyCounter.LevelMoney.Val += amount;
                MoneyCounter.TotalMoney.Val += amount;
            });
       
        }
    }
}