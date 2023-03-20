#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Levels
{
    [CustomEditor(typeof(LevelManager))]
    public class LevelManagerEditor : Editor
    {
        private float _switchWidth = 88;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var me = target as LevelManager;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("<<", GUILayout.Width(_switchWidth)))
            {
                me.EditorPrev();
                EditorUtility.SetDirty(me);
            }

            if (GUILayout.Button(">>", GUILayout.Width(_switchWidth)))
            {
                me.EditorNext();
                EditorUtility.SetDirty(me);
            }

            GUILayout.EndHorizontal();

            if (GUILayout.Button("ClearPrefs", GUILayout.Width(200)))
            {
                PlayerPrefs.DeleteAll();
                EditorUtility.SetDirty(me);
            }
        }
    }

#endif

}