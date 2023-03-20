using UnityEngine;

namespace LittleTricks
{
    public class EditorPoint : MonoBehaviour
    {
        [SerializeField] private float _rad;
        [SerializeField] private Color _color;


        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _rad);
        }
        #endif
    }
    
}