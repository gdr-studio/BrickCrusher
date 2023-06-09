#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameUI
{
    [CustomEditor(typeof(UIManager))]
    public class UIManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var me = target as UIManager;
            if (GUILayout.Button("close all"))
            {
                me.Close();
            }
        }
    }
}
#endif