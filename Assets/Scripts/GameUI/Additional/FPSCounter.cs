using System.Collections;
using TMPro;
using UnityEngine;

namespace GameUI.Additional
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _pollTime;
        [SerializeField] private bool _activate = true;
        private Coroutine _fpsCounting;
        void Start()
        {
            if (_activate)
            {
                _fpsCounting = StartCoroutine(FPSCounting());
                if (_text != null)
                    _text.gameObject.SetActive(true);
            }
            else
            {
                if (_text != null)
                    _text.gameObject.SetActive(false);
            }
        }

        private IEnumerator FPSCounting()
        {
            float elpased = 0f;
            int frameCount = 1;
            while (true)
            {
                if (elpased >= _pollTime)
                {
                    elpased = 0;
                    frameCount = Mathf.RoundToInt(1f/_pollTime * frameCount);
                    if(_text != null)
                        _text.text = frameCount.ToString();
                    frameCount = 1;
                }
                frameCount++;
                elpased += Time.deltaTime;
                yield return null;
            }
           
        }


    }
}