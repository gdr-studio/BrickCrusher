using System.Collections.Generic;
using UnityEngine;

namespace LittleTricks
{
    public static class Destroyer
    {
        public static void ClearGOList<T>(List<T> list) where T: MonoBehaviour
        {
            foreach (var mono in list)
            {
                Object.DestroyImmediate(mono.gameObject);
            }
            list.Clear();
        }
        
        public static void ClearGOList(List<GameObject> list) 
        {
            foreach (var mono in list)
            {
                Object.DestroyImmediate(mono.gameObject);
            }
            list.Clear();
        }
    }
}