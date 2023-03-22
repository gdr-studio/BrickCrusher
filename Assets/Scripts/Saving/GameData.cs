using System.Collections.Generic;

namespace Saving
{
    [System.Serializable]
    public class GameData
    {
        public int CurrentLevelIndex;
        public int TotalLevelsPassed;
        public int MoneyCount;

        public GameData()
        {
            CurrentLevelIndex = TotalLevelsPassed = 0;
        }
        
    }
}