namespace Levels
{
    public interface ILevelManager
    {
        public void Init();
        void LoadLevel(int levelIndex);
        void RestartLevel();
        void NextLevel();
        void LoadLast();
    }
}