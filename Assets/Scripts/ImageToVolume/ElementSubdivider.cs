using System.Collections.Generic;
using Data;
using UnityEngine;
using Zenject;

namespace ImageToVolume
{
    public class ElementSubdivider : MonoBehaviour
    {
        public Renderer renderer;
        public List<ElementSubDropData> dropData;
        public List<ElementSub> spawnedParts;
        public Layers layers;
        public MainGameConfig config;
        
        [Inject] private ElementSubPool _pool;
        
        public void SetData(List<ElementSubDropData> data)
        {
            dropData = data;
        }

        public void SpawnAndDrop()
        {
            transform.localEulerAngles = Vector3.zero;
            var scaleX = 1f / Mathf.Sqrt(dropData.Count) * transform.localScale.x * transform.parent.localScale.x;
            var scale = Vector3.one * scaleX;
            foreach (var data in dropData)
            {
                var instance = _pool.GetItem();
                instance.gameObject.SetActive(true);
                instance.transform.position = transform.TransformPoint(data.localPos);
                instance.DropAsRb();
                instance.transform.localScale = scale;
                spawnedParts.Add(instance);
                instance.gameObject.layer = layers.DroppedPiecesLayer;
                instance.colorSetter.SetMaterial(renderer.material, data.Tiling, data.Offset);
            }
        }

        public void DropAll()
        {
            foreach (var part in spawnedParts)
            {
                part.gameObject.layer = layers.DroppedPiecesLayer;
                part.DropAsRb();
            }            
        }

        public void PushRandom()
        {
            foreach (var part in spawnedParts)
            {
                var force = UnityEngine.Random.onUnitSphere;
                force.z = 0;
                force += Vector3.down * config.ElementDownForceAdd;
                force *= config.ElementDropPushForce;
                part.rb.AddForce(force, ForceMode.VelocityChange);
                part.rb.AddTorque(force, ForceMode.VelocityChange);
            }
        }
    }
}