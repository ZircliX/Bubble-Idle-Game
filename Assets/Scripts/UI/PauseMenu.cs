using UnityEngine;

namespace BubbleIdle.Core
{
    public class PauseMenu : MonoBehaviour
    
    {
  
        public GameObject PausePanel;

        void Update ()
        {
            
        }


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
