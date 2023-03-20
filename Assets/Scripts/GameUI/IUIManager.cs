namespace GameUI
{
    public interface IUIManager
    {
        void Init();
        void CloseAll();
        void ShowStart();
        void ShowProgress();
        void ShowWin();
        void ShowFail();
    }
}