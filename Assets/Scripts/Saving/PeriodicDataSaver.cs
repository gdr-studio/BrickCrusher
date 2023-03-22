using System.Collections;
using Data;
using UnityEngine;

namespace Saving
{
    public class PeriodicDataSaver : MonoBehaviour
    {
        public bool DoSave = true;
        [SerializeField] private DataSaver _saver;
        [SerializeField] private MainGameConfig _config;

        private void Start()
        {
            StartCoroutine(PeriodicSaving(_config.DataSavePeriod));
        }

        private IEnumerator PeriodicSaving(float period)
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(period);
                try
                {
                    if(DoSave)
                        _saver.SaveData();
                }
                catch(System.Exception ex)
                {
                    Debug.Log($"Can't save!. {ex.Message}\n {ex.StackTrace}");
                }
            }
        }
    }
}