using System.IO;
using Data.Game;
using Helpers;
using Merging;
using Money;
using UnityEngine;

namespace Saving
{
    [CreateAssetMenu(fileName = nameof(GameDataSaver), menuName = "SO/" + nameof(GameDataSaver))]
    public class GameDataSaver : DataSaver
    {
        private const string FileName = "SavedGameData";
        
        public PlayerWeaponCollection playerWeapons;
        private GameData _loadedData;
        
        public override GameData LoadedData => _loadedData;
        private string Path => Application.persistentDataPath + "/" + FileName;
        
        public override void SaveData()
        {
            var data = new GameData();
            SaveLevel(data);
            SaveMoney(data);
            
            var jsonString = JsonUtility.ToJson(data);
            File.WriteAllText(Path, jsonString);
        }

        public override void LoadData()
        {
            if (File.Exists(Path))
            {
                var fileContents = File.ReadAllText(Path);
                _loadedData = JsonUtility.FromJson<GameData>(fileContents);
            }
            else
            {
                _loadedData = new GameData();
            }
            InitLevel();
            InitMoney();
            InitWeapons();
        }

        private void SaveLevel(GameData data)
        {
            data.CurrentLevelIndex = GlobalData.LevelIndex;
            data.TotalLevelsPassed = GlobalData.LevelTotal;
        }

        private void SaveMoney(GameData data)
        {
            data.MoneyCount = MoneyCounter.TotalMoney.Val;
        }

        
        private void InitLevel()
        {
            GlobalData.LevelIndex = _loadedData.CurrentLevelIndex;
            GlobalData.LevelTotal = _loadedData.TotalLevelsPassed;
        }

        private void InitMoney()
        {
            MoneyCounter.TotalMoney.Val = _loadedData.MoneyCount;
        }

        private void InitWeapons()
        {
               
        }
        

        #if UNITY_EDITOR
        public void DebugPath()
        {
            Dbg.Green("Saved FILE:   " + Path);
            Dbg.Green("PDT:  " + Application.persistentDataPath);
        }
        #endif
    }
}