using UnityEngine;
using TMPro;
using System;
using GameUI.Buttons;

namespace GameUI
{
    public class UIPageWithButton : UIPage
    {
        [SerializeField] protected TextMeshProUGUI _header;
        [SerializeField] protected TextMeshProUGUI _btnText;
        [SerializeField] protected ProperButton _button;
        public event Action OnButtonClick;

        public virtual void OnClick()
        {   
            OnButtonClick?.Invoke();
        }
        
        public virtual void SetHeader(string text)
        {
            _header.text = text;
        }
        
        public virtual void SetButtonText(string text)
        {
            _btnText.text = text;
        }
    }
}