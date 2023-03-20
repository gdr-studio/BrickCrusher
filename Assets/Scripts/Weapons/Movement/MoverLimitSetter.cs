using Helpers;
using UnityEngine;

namespace Weapons.Movement
{
    public class MoverLimitSetter : MonoBehaviour
    {
        public WeaponOneDMover mover;
        [Range(0f,1f)] public float offset = 0.1f;
        
  

        public void SetLimits()
        {
            var cam = Camera.main;
            var pos = mover.moveTarget.transform.position;
            var camPosVec = pos - cam.transform.position;
            // Debug.DrawLine(cam.transform.position, cam.transform.position + camPosVec * 10, Color.blue, 10f);
            var viewPortPos = cam.WorldToViewportPoint(pos);
            var worldCornerPos = cam.ViewportToWorldPoint(new Vector3((1-offset), viewPortPos.y, camPosVec.z));
            // Debug.DrawLine(cam.transform.position, worldCornerPos, Color.red, 10f);
            mover.Limit = worldCornerPos.x;

        }
        
        
    }
}