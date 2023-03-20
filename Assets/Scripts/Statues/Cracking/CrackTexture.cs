using Services.Pool;
using UnityEngine;

namespace Statues.Cracking
{
    public class CrackTexture : MonoBehaviour, IPooledObject<CrackTexture>
    {
        public float forwardOffset;
        public float unitScale = 0.1f;
        private IPool<CrackTexture> _pool;
        
        public CrackTexture GetObject() => this;
        
        public void ShowAt(Vector3 position, float scaleX, float scaleZ, Vector3 offsetDir)
        {
            transform.localScale = Vector3.one * unitScale * scaleX;
            gameObject.SetActive(true);
            transform.position = position + offsetDir * (forwardOffset + scaleZ/2);
        }

        public void Rotate(Quaternion rotation)
        {
            var n = UnityEngine.Random.Range(0, 4);
            transform.rotation = rotation * Quaternion.Euler(0f, 0f, n * 90f);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _pool.Return(this);
        }
        
        public void Init(IPool<CrackTexture> pool)
        {
            _pool = pool;
        }

        public void HideToCollect()
        {
            gameObject.SetActive(false);
        }
    }
}