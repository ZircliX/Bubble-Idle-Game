using System;
using UnityEngine;

namespace BubbleIdle
{
    public static class GameController
    {
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
            Application.wantsToQuit += UnLoad();

            //Calculer les gains hors ligne
        }

        private static Func<bool> UnLoad()
        {
            Debug.Log("Quit Game");
            return () => true;
        }
    }
}