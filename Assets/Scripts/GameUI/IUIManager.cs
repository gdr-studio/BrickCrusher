namespace GameUI
{
    public interface IUIManager
    {
        void Init();
        void Close();
        void ShowStart();
        void ShowProgress();
        void ShowWin();
        void ShowFail();
    }
}