using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Services.Pool
{
    public class MonoPool<T> : MonoBehaviour, IPool<T> where T : MonoBehaviour, IPooledObject<T>
    {
        public T Prefab;
        public int initAmount = 30;
        public int addAmount = 30;
        public int availableCount = 0;
        public Transform parent;
        protected Dictionary<IPooledObject<T>, bool> _pool;
        [Inject] private DiContainer _container;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public virtual void Spawn()
        {
            // Debug.Log($"SPAWNING, {gameObject.name}");
            _pool = new Dictionary<IPooledObject<T>, bool>();
            var prefab = Prefab;
            for (var i = 0; i < initAmount; i++)
            {
                var instance = _container.InstantiatePrefabForComponent<T>(prefab.GetObject(), parent);
                _pool.Add( instance, true );
                InitObject(instance);
            }
            availableCount += initAmount;
        }

        public virtual void ExtendPool()
        {
            if(_pool == null)
                _pool = new Dictionary<IPooledObject<T>, bool>();
            var prefab = Prefab;
            for (var i = 0; i < addAmount; i++)
            {
                var instance = _container.InstantiatePrefabForComponent<T>(prefab.GetObject(), parent);
                _pool.Add( instance, true );
                InitObject(instance);
            }
            availableCount += addAmount;
        }
        
        public virtual T GetItem()
        {
            availableCount--;
            if (availableCount <= 1)
            {
                ExtendPool();
            }
            var result = _pool.First(t => t.Value);
            _pool[result.Key] = false;
            return result.Key.GetObject();
        }

        public virtual void Return(T target)
        {
            if (_pool[target] == true)
                return;
            _pool[target] = true;
            availableCount++;
        }

        public void CollectAllBack()
        {
            foreach (var pair in _pool)
            {
                if (pair.Value == false)
                {
                    pair.Key.HideToCollect();
                    _pool[pair.Key] = true;
                }
            }
            availableCount = _pool.Count;
        }

        protected virtual void InitObject(T obj)
        {
            obj.Init(this);
            obj.gameObject.SetActive(false);
        }
    
    }
}