using BubbleIdle.SeaweedSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BubbleIdle.BubbleSystem
{
    public class SpecialBubble : Bubble
    {
        public override void Initialize(SeaweedData data, int seaweedLevel)
        {
            EventManager.Instance.SpawnBubble();
            this.data = data;
            this.seaweedLevel = seaweedLevel;
            initialPosition = transform.position;
            
            transform.StartBubbleMovement(initialPosition, moveDuration, swingAmplitude, swingSpeed);
            Destroy(gameObject, moveDuration);
        }
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            int value = Mathf.RoundToInt(data.bubbleValue);
            
            transform.DOKill();
            VFXManager.Instance.PlayVFX("PopBubble", transform.position);
            FeedbackCounter.Instance.SpawnCounter(transform.position, value);
            
            EventManager.Instance.ClickBubble();
            GameController.ResourcesManager.AddSpecialBubbles(value);
            Destroy(gameObject);
        }
    }
    
}
