#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Saving
{
    [CustomEditor(typeof(GameDataSaver))]
    public class GameDataSaverEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var me = target as GameDataSaver;
            base.OnInspectorGUI();
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save"))
            {
                me.SaveData();
            }

            if (GUILayout.Button($"Load"))
            {
                me.LoadData();
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button($"ClearAll"))
            {
                me.ClearData();
            }
            if (GUILayout.Button("Path"))
            {
                me.DebugPath();
            }
        }
    }
}
#endif