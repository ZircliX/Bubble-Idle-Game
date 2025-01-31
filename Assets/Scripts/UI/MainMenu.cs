using UnityEngine;
using UnityEngine.SceneManagement;

namespace BubbleIdle.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Change la scène spécifiée par "sceneName"
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}