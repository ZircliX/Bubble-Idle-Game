using BubbleIdle.SeaweedSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BubbleIdle.BubbleSystem
{
    public class Bubble : MonoBehaviour, IPointerDownHandler
    {
        protected SeaweedData data;
        protected int seaweedLevel;
        
        [Header("Settings")]
        [SerializeField] protected float moveDuration = 10f; // Durée totale du mouvement
        [SerializeField] protected float swingAmplitude = 1f; // Amplitude du balancement
        [SerializeField] protected float swingSpeed = 30f; // Vitesse du balancement

        protected Vector3 initialPosition;
        
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
            int value = Mathf.RoundToInt(data.bubbleValue * Mathf.Pow(data.costMultiplier, seaweedLevel));
            
            transform.DOKill();
            VFXManager.Instance.PlayVFX("PopBubble", transform.position);
            FeedbackCounter.Instance.SpawnCounter(transform.position, value);
            
            EventManager.Instance.ClickBubble();
            GameController.ResourcesManager.AddBubbles((value * GameController.ResourcesManager.ProductionBonus).ToString());
            Destroy(gameObject);
        }
    }
}