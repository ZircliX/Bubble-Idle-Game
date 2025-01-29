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
            
            EventManager.Instance.OnSeaweedBuySound += () => PlayClip(nameof(EventManager.OnSeaweedBuySound));
            EventManager.Instance.OnSeaweedUpgrade += () => PlayClip(nameof(EventManager.OnSeaweedUpgrade));
            
            EventManager.Instance.OnMoneyChange += () => PlayClip(nameof(EventManager.OnMoneyChange));
        }

        private void PlayClip(string clipName)
        {
            AudioName clipData = audioClips
                .FirstOrDefault(data => data.name == clipName);

            if (clipData.needPitch) audioSource.pitch = Random.Range(0.7f, 1.3f);
            else audioSource.pitch = 1;
            
            audioSource.PlayOneShot(clipData.clip);
        }
    }

    [System.Serializable]
    public struct AudioName
    {
        public string name;
        public AudioClip clip;
        public bool needPitch;
    }
}