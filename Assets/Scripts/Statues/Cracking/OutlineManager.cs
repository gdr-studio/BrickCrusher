using UnityEditor;
using UnityEngine;

namespace Statues.Cracking
{
    public class OutlineManager : MonoBehaviour
    {
        public Sprite sprite;

        public void Generate()
        {
            var texture = sprite.texture;
            var pixels = sprite.texture.GetPixels();
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.blue;
            }
            texture.SetPixels(pixels);
        }
    }
    
    
    
    #if UNITY_EDITOR

    [CustomEditor(typeof(OutlineManager))]
    public class OutlineManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var me = target as OutlineManager;

            if (GUILayout.Button("Generate"))
            {
                me.Generate();
            }
        }
    }
    
    #endif
}