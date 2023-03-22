#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Merging
{
    [CustomEditor(typeof(MergingPurchaser))]
    public class MergingPurchaserEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var me = target as MergingPurchaser;
            if (GUILayout.Button($"GetParts"))
            {
                me.GetParts();
                EditorUtility.SetDirty(me);
            }
        }
    }
}
#endif