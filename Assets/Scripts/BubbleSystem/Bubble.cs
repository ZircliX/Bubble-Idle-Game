using BubbleIdle.SeaweedSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BubbleIdle.BubbleSystem
{
    public class Bubble : MonoBehaviour, IPointerDownHandler
    {
        private SeaweedData data;
        private int seaweedLevel;
        
        [Header("Settings")]
        [SerializeField] private float moveDuration = 10f; // Durée totale du mouvement
        [SerializeField] private float swingAmplitude = 1f; // Amplitude du balancement
        [SerializeField] private float swingSpeed = 30f; // Vitesse du balancement

        private Vector3 initialPosition;
        
        public virtual void Initialize(SeaweedData data, int seaweedLevel)
        {
            EventManager.Instance.SpawnBubble();
            this.data = data;
            this.seaweedLevel = seaweedLevel;
            initialPosition = transform.position;
            
            transform.StartBubbleMovement(initialPosition, moveDuration, swingAmplitude, swingSpeed);
            Destroy(gameObject, moveDuration);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            int value = Mathf.RoundToInt(data.bubbleValue * Mathf.Pow(seaweedLevel, data.costMultiplier));
            
            transform.DOKill();
            VFXManager.Instance.PlayVFX("PopBubble", transform.position);
            FeedbackCounter.Instance.SpawnCounter(transform.position, value);
            
            EventManager.Instance.ClickBubble();
            GameController.ResourcesManager.AddBubbles(value.ToString());
            Destroy(gameObject);
        }
    }
}