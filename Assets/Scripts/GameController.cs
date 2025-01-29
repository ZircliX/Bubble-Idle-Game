using System;
using BubbleIdle.Core;
using BubbleIdle.SaveSystem;
using SaveSystem.Core;
using UnityEngine;

namespace BubbleIdle
{
    public static class GameController
    {
        public static event Action OnGameSave;
        
        public static ProgressionManager ProgressionManager { get; private set; }
        public static ResourcesManager ResourcesManager { get; private set; }
        private static GameMetrics gameMetrics;
        public static GameMetrics Metrics
        {
            get
            {
                if (!gameMetrics)
                    gameMetrics = Resources.Load<GameMetrics>("GameMetrics");

                return gameMetrics;
            }
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Load()
        {
            Application.targetFrameRate = 60;
            Application.quitting += UnLoad;
            
            ResourcesManager = new ResourcesManager();
            ProgressionManager = new ProgressionManager();
            Save.AddListener(ProgressionManager);
            Save.SetSaveManager(new SaveManager());

            LoadProgress();
        }

        private static void UnLoad()
        {
            Debug.Log("Quit Game");
            SaveProgress();
            Save.RemoveListener(ProgressionManager);
        }

        private static void SaveProgress()
        {
            Debug.Log("Saving player progress");

            Save.Push<SaveFile, SaveSettings>(new SaveSettings()
            {
                prefName = "Player"
            });
            
            OnGameSave?.Invoke();
        }

        private static void LoadProgress()
        {
            Debug.Log("Loading player progress");
            Save.Pull<SaveFile, SaveSettings>(out _, new SaveSettings()
            {
                prefName = "Player"
            });
        }
    }
}