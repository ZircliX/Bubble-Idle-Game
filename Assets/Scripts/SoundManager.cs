using System.Linq;
using UnityEngine;

namespace BubbleIdle
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioName[] audioClips;
        
        private void OnEnable()
        {
            EventManager.Instance.OnBubbleSpawn += () => PlayClip(nameof(EventManager.OnBubbleSpawn));
            EventManager.Instance.OnBubbleClick += () => PlayClip(nameof(EventManager.OnBubbleClick));
            
            EventManager.Instance.OnSeaweedBuy += () => PlayClip(nameof(EventManager.OnSeaweedBuy));
            EventManager.Instance.OnSeaweedUpgrade += () => PlayClip(nameof(EventManager.OnSeaweedUpgrade));
            
            EventManager.Instance.OnMoneyAddSound += () => PlayClip(nameof(EventManager.OnMoneyAddSound));
        }

        private void PlayClip(string name)
        {
            AudioClip clip = audioClips
                .Where(data => data.name == name)
                .Select(data => data.clip)
                .FirstOrDefault();

            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    [System.Serializable]
    public struct AudioName
    {
        public string name;
        public AudioClip clip;
    }
}