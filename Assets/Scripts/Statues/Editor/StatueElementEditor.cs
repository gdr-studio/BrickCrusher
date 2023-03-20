#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Statues.Editor
{
    [CustomEditor(typeof(StatueElement))]
    public class StatueElementEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var me = target as StatueElement;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("UpdateColor"))
            {
                me.colorSetter.UpdateColor();
            }
            if (GUILayout.Button("TilingUpdate"))
            {
                me.colorSetter.UpdateTilingAndOffset();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("NeighboursOn"))
            {
                me.NeighboursOn(true);
            }
            if (GUILayout.Button("NeighboursOff"))
            {
                me.NeighboursOn(false);
            }
            GUILayout.EndHorizontal();
        }
    }
}
#endif