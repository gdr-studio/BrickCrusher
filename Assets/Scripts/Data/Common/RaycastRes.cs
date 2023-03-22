using UnityEngine;

namespace Data
{
    public struct RaycastRes
    {
        public Collider Collider;
        public Vector3 Point;
        public Vector3 Normal;

        public RaycastRes(RaycastHit hit)
        {
            Collider = hit.collider;
            Point = hit.point;
            Normal = hit.normal;
        }
    }
}