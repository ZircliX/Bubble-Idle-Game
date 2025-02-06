using UnityEngine;

namespace BubbleIdle
{
    public static class AppCloseDetector
    {
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            // Create a hidden GameObject to host the MonoBehaviour
            var holder = new GameObject("AppLifecycleHolder");
            holder.AddComponent<AppLifecycleForwarder>();
            Object.DontDestroyOnLoad(holder);
        }
    }

    public class AppLifecycleForwarder : MonoBehaviour
    {
        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
            {
                Debug.Log("App paused (sent to background)");
                GameController.UnLoad();
            }
        }

        private void OnApplicationQuit()
        {
            Debug.Log("App quit");
            GameController.UnLoad();
        }
    }
}