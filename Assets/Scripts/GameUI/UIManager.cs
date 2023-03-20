using UnityEngine;
namespace GameUI
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private UIPage _start;
        [SerializeField] private UIPage _finish;
        [SerializeField] private UIPage _fail;
        [SerializeField] private UIPage _prog;

        // is disabled
        public void Init()
        {
            CloseAll();
            _start.ShowPage(false);
        }

        public void CloseAll()
        {
            _fail.HidePage(true);
            _start.HidePage(true);
            _finish.HidePage(true);
            _prog.HidePage(true);
        }

        public void ShowStart()
        {
            CloseAll();
            _start.ShowPage(false);
        }

        public void ShowProgress()
        {
            CloseAll();
            _prog.ShowPage(false);
        }

        public void ShowWin()
        {
            CloseAll();
            _finish.ShowPage(false);
        }

        public void ShowFail()
        {
            CloseAll();
            _fail.ShowPage(false);
        }

  
    }
}