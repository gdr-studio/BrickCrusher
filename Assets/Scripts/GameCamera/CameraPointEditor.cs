#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameCamera
{
    [CustomEditor(typeof(CameraPoint))]
    public class CameraPointEditor : Editor
    {
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var me = target as CameraPoint;
            
            if (me.AutoSetInEditor && Application.isPlaying == false)
            {
                me.SetToThisPoint();
            }
            
            if (GUILayout.Button("SetThisPoint"))
            {
                me.SetToThisPoint();
            }
            if (GUILayout.Button("MoveToThisPoint"))
            {
                me.MoveToThisPoint();
            }
            if (GUILayout.Button("FollowThisPoint"))
            {
                me.FollowThisPoint();
            }
        }
    }
}
#endif