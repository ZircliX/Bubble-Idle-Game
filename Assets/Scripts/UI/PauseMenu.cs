using UnityEngine;

namespace BubbleIdle.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject PausePanel;
        
        public void Pause()
        {
            PausePanel.SetActive(true);
        }

        public void Continue()
        {
            PausePanel.SetActive(false);
        }
    }
}