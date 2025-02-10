using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BubbleIdle.Core
{
    public class Tuto : MonoBehaviour
    {
        [SerializeField] private Image[] images;
        [SerializeField] private Button button;
        private int currentIndex;

        private void Awake()
        {
            for (int i = 1; i < images.Length; i++)
            {
                images[i].gameObject.SetActive(false);
            }
        }

        public void OnNext()
        {
            currentIndex++;
            
            for (int i = 0; i < images.Length; i++)
            {
                if (currentIndex == images.Length)
                {
                    Play();
                    break;
                }

                if (i == currentIndex)
                    images[i].gameObject.SetActive(true);
                else
                    images[i].gameObject.SetActive(false);
                        
                if (currentIndex == images.Length - 1)
                    button.GetComponentInChildren<TMP_Text>().text = "Play";
            }
        }

        public void Play()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
