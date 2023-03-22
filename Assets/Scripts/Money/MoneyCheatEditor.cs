#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Money
{
    [CustomEditor(typeof(MoneyCheat))]
    public class MoneyCheatEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var me = target as MoneyCheat;
            if (GUILayout.Button("Set 0"))
            {
                me.Clear();
            }
         
            if (GUILayout.Button("AddMoney"))
            {
                me.Add();   
            }
            
        }
    }
}
#endif