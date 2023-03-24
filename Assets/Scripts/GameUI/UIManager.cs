using UnityEngine;
namespace GameUI
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private UIPage _start;
        [SerializeField] private UIPage _finish;
        [SerializeField] private UIPage _fail;
        [SerializeField] private UIPage _prog;
        [SerializeField] private UIPage _money;
        [SerializeField] private UIPage _mering;
        [SerializeField] private UIPage _tutorial;
        
        public void Init()
        {
            Close();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void Close()
        {
            _fail.HidePage(true);
            _start.HidePage(true);
            _finish.HidePage(true);
            _prog.HidePage(true);
            _money.HidePage(true);
            _mering.HidePage(true);
            _tutorial.HidePage(true);
        }

        public void ShowStart()
        {
            Close();
            _start.ShowPage(false);
        }

        public void ShowProgress()
        {
            Close();
            _prog.ShowPage(false);
        }

        public void ShowWin()
        {
            Close();
            _finish.ShowPage(false);
        }

        public void ShowFail()
        {
            Close();
            _fail.ShowPage(false);
        }

  
    }
}