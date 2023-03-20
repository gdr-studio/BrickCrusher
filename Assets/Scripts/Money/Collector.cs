using UnityEngine;

namespace Money
{
    public class Collector : MonoBehaviour
    {
        public void Collect(ICollectable collectable)
        {
            try
            {
                collectable.Collect();
            }
            catch (System.Exception ex)
            {
                Debug.Log($"EXCEPTION: {ex.Message}\nTrace: {ex.StackTrace}");
            }
        }
    }
}