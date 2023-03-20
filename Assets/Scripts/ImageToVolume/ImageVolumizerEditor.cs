#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ImageToVolume
{
    [CustomEditor(typeof(ImageVolumizer))]
    public class ImageVolumizerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var me = target as ImageVolumizer;
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button($"Clear"))
            {
                me.Clear();
            }
            if (GUILayout.Button($"Count Pixels"))
            {
                me.GetSize();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button($"1. ByTiling"))
            {
                me.CreateByTiling();
            }
            if (GUILayout.Button($"1. ByEachPixel"))
            {
                me.CreateByPixel();
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button($"2. SetupNeighbours"))
            {
                me.SetupNeighbours();
            }
      

        }
    }
}
#endif