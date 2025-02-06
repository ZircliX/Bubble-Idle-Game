using System.Collections;
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
        private IEnumerator Start()
        {
            while (true)
            {
                GameController.SaveProgress();
                yield return new WaitForSeconds(60);
            }
        }
        
        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
            {
                Debug.Log("App paused (sent to background)");
                GameController.SaveProgress();
            }
        }

        private void OnApplicationQuit()
        {
            Debug.Log("App quit");
            GameController.UnLoad();
        }
    }
}