using Data;
using ImageToVolume;
using UnityEngine;

namespace Statues
{
    public class BreakablePiece : MonoBehaviour
    {
        public Collider coll;
        public Rigidbody rb;
        public Layers layers;
        public MainGameConfig config;
        public VolumeElement element;
        
        public bool Broken { get; set; }
        
        public void SetTriggerOnly()
        {
            coll.isTrigger = true;
            rb.isKinematic = true;
            coll.enabled = true;
        }

        public void Activate()
        {
            coll.enabled = true;
            coll.isTrigger = false;
            rb.isKinematic = false;
        }

        public void HideSelf()
        {
            coll.enabled = false;
            rb.isKinematic = true;
        }
        
        public void Break()
        {
            Activate();
            gameObject.layer = layers.DroppedPiecesLayer;
            Broken = true;
            
        }

        public void CheckNeighbours()
        {
            foreach (var ni in element.neighbourIndices)
            {
                
            }
        }

        public void PushRandom()
        {
            var vec = UnityEngine.Random.onUnitSphere;
            vec *= config.ElementDropPushForce;
            rb.AddForce(vec, ForceMode.Force);
        }
    }
}
