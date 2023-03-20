using UnityEngine;

namespace Game.View.Impl
{
    public partial class BreakablePart
    {
        [System.Serializable]
        public class BrokenPiece
        {
            public Rigidbody Rb;
            public Collider Coll;

            private bool _active;
            public bool Active
            {
                get => _active;
                set
                {
                    _active = value;
                    if (_active)
                    {
                        Rb.isKinematic = false;
                        Coll.enabled = true;
                        Rb.gameObject.SetActive(true);
                    }
                    else
                    {
                        Rb.isKinematic = true;
                        Coll.enabled = false;
                        Rb.gameObject.SetActive(false);
                    }
                }
            }

            public BrokenPiece(Rigidbody rb, Collider coll)
            {
                Rb = rb;
                Coll = coll;
            }

            public void Reparent(Transform parent)
            {
                Rb.transform.SetParent(parent);
            }
            public void Push(Vector3 force)
            {
                Active = true;
                Rb.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}