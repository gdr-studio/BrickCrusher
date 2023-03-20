#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Statues.Editor
{
    [CustomEditor(typeof(BlockStatue))]
    public class BlockStatueEdtiro : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var me = target as BlockStatue;
            if (GUILayout.Button($"InitColors"))
            {
                me.UpdateAll();
            }
 
        }
    }
}
#endif