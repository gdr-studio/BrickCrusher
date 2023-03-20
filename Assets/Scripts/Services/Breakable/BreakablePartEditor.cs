#if UNITY_EDITOR
using Game.View.Impl;
using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    [CustomEditor(typeof(BreakablePart))]
    public class BreakablePartEditor : Editor
    {
        private BreakablePart me;

        private void OnEnable()
        {
            me = target as BreakablePart;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Get"))
            {
                me.GetParts();
            }
            if (GUILayout.Button("Passive"))
            {
                me.SetPassive();
            }
        }
    }
}
#endif