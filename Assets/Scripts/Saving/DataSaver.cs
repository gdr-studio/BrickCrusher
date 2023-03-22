using UnityEngine;

namespace Saving
{
    public abstract class DataSaver : ScriptableObject
    {
        public abstract void SaveData();
        public abstract void LoadData();
        public abstract GameData LoadedData { get; }
    }
}