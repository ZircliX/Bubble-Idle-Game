using BubbleIdle.SaveSystem;
using UnityEngine;

namespace BubbleIdle
{
    public static class GameController
    {
        public static ProgressionManager ProgressionManager { get; private set; }
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
            
            ProgressionManager = new ProgressionManager();

            //Calculer les gains hors ligne
        }

        private static void UnLoad()
        {
            Debug.Log("Quit Game");
        }
    }
}