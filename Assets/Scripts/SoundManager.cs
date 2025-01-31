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
            //Bubbles
            EventManager.Instance.OnBubbleSpawn += () => PlayClip(nameof(EventManager.Instance.OnBubbleSpawn));
            EventManager.Instance.OnBubbleClick += () => PlayClip(nameof(EventManager.Instance.OnBubbleClick));
            
            //Seaweeds
            EventManager.Instance.OnSeaweedBuySound += () => PlayClip(nameof(EventManager.Instance.OnSeaweedBuySound));
            EventManager.Instance.OnSeaweedUpgrade += () => PlayClip(nameof(EventManager.Instance.OnSeaweedUpgrade));
            
            //UI
            EventManager.Instance.OnUIOpen += () => PlayClip(nameof(EventManager.Instance.OnUIOpen));

        }

        private void PlayClip(string clipName)
        {
            AudioName clipData = audioClips
                .FirstOrDefault(data => data.name == clipName);

            if (clipData.needPitch) audioSource.pitch = Random.Range(0.7f, 1.3f);
            else audioSource.pitch = 1;
            audioSource.volume = clipData.volume;

            AudioClip clip;
            if (clipData.clips.Length == 1) clip = clipData.clips[0];
            else clip = clipData.clips[Random.Range(0, clipData.clips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    [System.Serializable]
    public struct AudioName
    {
        public string name;
        public AudioClip[] clips;
        public bool needPitch;
        [Range(0, 1)] public float volume;
    }
}