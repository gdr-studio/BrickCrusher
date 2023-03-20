using UnityEngine;

namespace Game.Sound.Data
{
    [System.Serializable]
    public class SoundSource
    {
        public AudioSource AudSource;
        public GameObject Go;
        public Transform DefaultParent;

        public void Reset()
        {
            #if UNITY_EDITOR
            if(AudSource == null)
                return;
            #endif
            
            AudSource.Stop();
            AudSource.clip = null;
            AudSource.spatialBlend = 0f;
            AudSource.maxDistance = 250;
            AudSource.transform.SetParent(DefaultParent);
        }

    }
}