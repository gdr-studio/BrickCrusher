using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class UIPage : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas;
        [SerializeField] protected GraphicRaycaster _raycaster;

        protected bool _isOpen;
        
        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                _isOpen = value;
                if (value)
                {
                    _canvas.enabled = true;
                    _raycaster.enabled = true;
                }
                else
                {
                    _canvas.enabled = false;
                    _raycaster.enabled = false;   
                }
            }
        }

        public virtual void ShowPage(bool fast)
        {
            IsOpen = true;
        }

        public virtual void HidePage(bool fast)
        {
            IsOpen = false;
        }
        
    }
}