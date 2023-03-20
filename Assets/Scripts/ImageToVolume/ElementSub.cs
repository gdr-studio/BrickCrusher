using Services.Pool;
using UnityEngine;

namespace ImageToVolume
{
    public class ElementSub : MonoBehaviour, IPooledObject<ElementSub>
    {
        public ColorSetter colorSetter;
        public Rigidbody rb;
        public Collider coll;
        public BlockCracker cracker;
        private const float Mass = 500;
        private IPool<ElementSub> _pool;
        private bool _haveRb;
        public void SetSize(float size)
        {
            transform.localScale = new Vector3(size,size,1f);
        }
        
        public void DropAsRb()
        {
            cracker.Hide();
            if (!_haveRb)
            {
                _haveRb = true;
                rb = gameObject.AddComponent<Rigidbody>();
            }
            rb.velocity = Vector3.zero;
            rb.mass = Mass;
            rb.isKinematic = false;
            coll.enabled = true;
            coll.isTrigger = false;
        }

        public void Init(IPool<ElementSub> pool)
        {
            _pool = pool;
        }

        public ElementSub GetObject() => this;

        public void HideAndReturn()
        {
            HideToCollect();
            if(_pool != null)
                _pool.Return(this);
        }

        public void HideToCollect()
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            coll.enabled = false;
            gameObject.SetActive(false);   
        }
        
    }
}