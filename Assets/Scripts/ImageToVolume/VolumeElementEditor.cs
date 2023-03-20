#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ImageToVolume
{
    [CustomEditor(typeof(VolumeElement))]
    public class VolumeElementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var me = target as VolumeElement;
            if (GUILayout.Button("UpdateColor"))
            {
                me.colorSetter.UpdateColor();
            }

            if (GUILayout.Button("UpdateTexture"))
            {
                me.UpdateTilingAndOffset();
            }
        }
    }
}
#endif