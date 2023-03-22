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
            if (GUILayout.Button("Save"))
            {
                me.SaveData();
            }

            if (GUILayout.Button($"Load"))
            {
                me.LoadData();
            }

            if (GUILayout.Button("Path"))
            {
                me.DebugPath();
            }
        }
    }
}
#endif