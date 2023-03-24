using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class UIPage : MonoBehaviour
    {
        [SerializeField] private int _order = 1;
        [SerializeField] protected Canvas _canvas;
        [SerializeField] protected GraphicRaycaster _raycaster;

        protected bool _isOpen;
        
        public bool IsOpen
        {
            get => gameObject.activeInHierarchy;
            set
            {
                _isOpen = value;
                if (value)
                {
                    // _canvas.enabled = true;
                    // _raycaster.enabled = true;
                    gameObject.SetActive(true);
                }
                else
                {
                    // _canvas.enabled = false;
                    // _raycaster.enabled = false;   
                    gameObject.SetActive(false);
                }
            }
        }

        public virtual void ShowPage(bool fast)
        {
            IsOpen = true;
            _canvas.sortingOrder = _order;
        }

        public virtual void HidePage(bool fast)
        {
            IsOpen = false;
        }
        
    }
}