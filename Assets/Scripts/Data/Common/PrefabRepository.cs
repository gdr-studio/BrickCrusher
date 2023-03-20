using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    public abstract class PrefabRepository<TPrefab, TName> : ScriptableObject where TPrefab : MonoBehaviour where TName : ScriptableObject
    {
        public List<Data<TPrefab, TName>> data = new List<Data<TPrefab, TName>>();

        public virtual TPrefab GetPrefab(TName name)
        {
            return data.First(t => t.name == name).prefab;
        }


        [System.Serializable]
        public class Data<TPrefab, TName>
        {
            public TName name;
            public TPrefab prefab;   
        }
    }
}