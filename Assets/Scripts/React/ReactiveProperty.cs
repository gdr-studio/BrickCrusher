using System;
using System.Collections.Generic;
using UnityEngine;

namespace React
{
    [System.Serializable]
    public class ReactiveProperty<T> where T : IEquatable<T>
    {
        public bool UpdateChangeOnly;
        [SerializeField] protected T _val;
        protected List<Action<T>> _listeners = new List<Action<T>>();
        
        public T Val
        {
            get => _val;
            set
            {
                if (UpdateChangeOnly)
                {
                    if (!_val.Equals(value))
                    {
                        foreach (var listener in _listeners)
                        {
                            listener.Invoke(value);
                        }
                    }
                }
                else
                {
                    foreach (var listener in _listeners)
                    {
                        listener.Invoke(value);
                    }          
                }
                _val = value;
            }
        }

        public ReactiveProperty()
        {
            Val = default;
            _listeners = new List<Action<T>>();
        }
        
        public ReactiveProperty(T start)
        {
            Val = start;
            _listeners = new List<Action<T>>();
        }

        public void SubOnChange(Action<T> listener)
        {
            _listeners.Add(listener);
        }
        
        public void UnsubOnChange(Action<T> listener)
        {
            _listeners.Remove(listener);
        }
        

    }
}