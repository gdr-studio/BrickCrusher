#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace LevelBorders
{
    [CustomEditor(typeof(Borders))]
    public class BordersEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var me = target as Borders;
            if (GUILayout.Button("Init"))
            {
                me.Init();
                EditorUtility.SetDirty(me);
            }
        }
    }
}
#endif