using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Levels;
using Zenject;

namespace Levels
{


    public class LevelManager : MonoBehaviour, ILevelManager
    {
        const string TOTAL_PASSED_COUNT = "PassedLevelsCount";
        const string CURRENT_LEVEL_INDEX = "CurrentLevelIndex";

        [SerializeField] public bool editorMode;
        [SerializeField] public int DebugIndex;
        [SerializeField] public Level CurrentLoadedLevel;
        public List<Level> Levels = new List<Level>();
        [Inject] private DiContainer _container;

        public int TotalLevels
        {
            get
            {
                if (editorMode)
                    return DebugIndex;
                else
                    return PlayerPrefs.GetInt(TOTAL_PASSED_COUNT, 0);
            }
            set
            {
                if (!editorMode)
                    PlayerPrefs.SetInt(TOTAL_PASSED_COUNT, value);
            }
        }

        public int CurrentIndex
        {
            get
            {
                if (editorMode)
                {
                    return DebugIndex;
                }

                return PlayerPrefs.GetInt(CURRENT_LEVEL_INDEX, 0);
            }
            set
            {
                if (editorMode)
                {
                    DebugIndex = value;
                }
                else
                    PlayerPrefs.SetInt(CURRENT_LEVEL_INDEX, value);
            }
        }

        public void EditorNext()
        {
            DebugIndex++;
            DebugIndex = Mathf.Clamp(DebugIndex, 0, Levels.Count - 1);
            LoadLevel(DebugIndex);
        }

        public void EditorPrev()
        {
            DebugIndex--;
            DebugIndex = Mathf.Clamp(DebugIndex, 0, Levels.Count - 1);
            LoadLevel(DebugIndex);
        }


        public void Init()
        {
#if !UNITY_EDITOR
        editorMode = false;
#endif
        }

        public void LoadLast()
        {
            LoadLevel(CurrentIndex);
        }

        public void RestartLevel()
        {
            LoadLevel(CurrentIndex);
        }

        public void NextLevel()
        {
            TotalLevels++;
            LoadLevel(CurrentIndex + 1);
        }


        public void LoadLevel(int levelIndex)
        {
            levelIndex = GetCorrectedIndex(levelIndex);
            var level = Levels[levelIndex];
            Clear();
            SpawnLevel(level);
            DebugIndex = levelIndex;
        }


        private int GetCorrectedIndex(int levelIndex)
        {
            if (editorMode)
                return levelIndex > Levels.Count - 1 || levelIndex <= 0 ? 0 : levelIndex;
            var totalCount = TotalLevels;
            if (totalCount > Levels.Count - 1)
            {
                if (Levels.Count == 1)
                    return 0;
                Debug.Log("RANDOMIZING LEVELS");
                var level = CurrentIndex;
                while (level == CurrentIndex)
                {
                    level = UnityEngine.Random.Range(0, Levels.Count - 1);
                }

                return level;
            }

            return levelIndex;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void SpawnLevel(Level levelData)
        {
            if (!levelData) 
                return;
            if (Application.isPlaying)
            {
                var instance = _container.InstantiatePrefabForComponent<Level>(levelData, transform);
                CurrentLoadedLevel = instance.GetComponent<Level>();
                CurrentLoadedLevel.Init();
            }
            else
            {
#if UNITY_EDITOR
                var instance = PrefabUtility.InstantiatePrefab(levelData, transform) as Level;
                if (instance != null) 
                    CurrentLoadedLevel = instance.GetComponent<Level>();
#endif
            }

        }

        private void Clear()
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i).gameObject;
                if (Application.isPlaying)
                    Destroy(child);
                else
                    DestroyImmediate(child);
            }
        }

    }
}