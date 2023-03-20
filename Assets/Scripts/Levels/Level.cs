using UnityEngine;

namespace Levels
{
    public abstract class Level : MonoBehaviour
    {
        public abstract void Init();
        public abstract void StartLevel();
        
    }
}