//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Threading.Tasks;
//using System.Collections;
//using System.Collections.Generic;
//namespace FlyCut.UI
//{
//    public class ProgressBar : MonoBehaviour
//    {
//        [SerializeField] private Image _barFill;
//        [SerializeField] private float _fillSpeed = 1f;
//        [SerializeField] private bool _useText = true;
//        [SerializeField] private TextMeshProUGUI _text;
//        private float _currentAmount = 0f;
//        private Coroutine _barFilling;
//        private void Start()
//        {
//            _text.text = "";
//        }

//        private IEnumerator BarFilling(float startAmount, float difference)
//        {
//            float elapsed = 0f;
//            float time = Mathf.Abs(difference) / _fillSpeed;
//            float amount = 0;
//            while(elapsed <= time)
//            {
//                amount = Mathf.Lerp(0, difference, elapsed / time);
//                SetFill(amount + startAmount);
//                elapsed += Time.deltaTime;
//                yield return null;
//            }
//            amount = difference;
//            SetFill(amount + startAmount);
//        }

//        public void SetFill(float amount)
//        {
//            _currentAmount = amount;
//            _barFill.fillAmount = amount;
//            _text.text = Mathf.RoundToInt(amount * 100).ToString(); ;
//        }

//        public void Fill(float amount)
//        {
//            float difference = amount - _currentAmount;
//            if (Mathf.Abs(difference) > 3f/100)
//            {
//                if (_barFilling != null)
//                    StopCoroutine(_barFilling);
//                _barFilling = StartCoroutine(BarFilling(_currentAmount, difference));
//            }
//            else
//            {
//                if (_barFilling != null)
//                    StopCoroutine(_barFilling);
//                SetFill(amount);
//            }
//        }

//        public async void SetBarColor(Color color)
//        {
//            await Task.Delay(1000);
//        }
//    }
//}

using UnityEngine;

namespace GameUI
{
    public abstract class ProgressBar : MonoBehaviour
    {
        public abstract void Fill(float progress);
        public abstract void SetBarColor(Color color);
    }
}