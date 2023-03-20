#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Statues.Editor
{
    [CustomEditor(typeof(BreakablePiece))]
    public class SmallPieceEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var me = target as BreakablePiece;
            if (GUILayout.Button($"TriggerOnly"))
            {
                me.SetTriggerOnly();
            }
            if (GUILayout.Button($"Activate"))
            {
                me.Activate();
            }
 
        }
    }
}
#endif