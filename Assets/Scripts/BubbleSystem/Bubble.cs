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
        [SerializeField] private float moveDuration = 2f; // Durée totale du mouvement
        [SerializeField] private float swingAmplitude = 0.5f; // Amplitude du balancement
        [SerializeField] private float swingSpeed = 5f; // Vitesse du balancement

        private Vector3 initialPosition;
        
        public void Initialize(SeaweedData data, int seaweedLevel)
        {
            EventManager.Instance.SpawnBubble();
            this.data = data;
            this.seaweedLevel = seaweedLevel;
            initialPosition = transform.position;
            StartBubbleMovement();
        }

        void StartBubbleMovement()
        {
            // Randomizing swing speed and amplitude for variation
            swingSpeed += Random.Range(-0.8f, 0.8f);
            swingAmplitude += Random.Range(-0.8f, 0.8f);
            moveDuration += Random.Range(-5f, 2f);
            
            transform.localScale = Vector3.one * 0.75f + Vector3.one * Random.Range(-0.3f, 0.3f);
            
            // Mouvement vertical principal
            transform.DOMoveY(initialPosition.y + 20f, moveDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => Destroy(gameObject));
            
            // Mouvement de swing horizontal
            int negativeSwing = Random.Range(0, 2) == 1 ? -1 : 1;
            DOTween.To(() => 0f, x => {
                    float swingOffset = Mathf.Sin(x * swingSpeed) * swingAmplitude;
                    transform.position = new Vector3(
                        initialPosition.x + swingOffset * negativeSwing,
                        transform.position.y,
                        transform.position.z
                    );
                }, 1f, moveDuration)
                .SetEase(Ease.Linear);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOKill();
            VFXManager.Instance.PlayVFX("PopBubble", transform.position);
            
            EventManager.Instance.ClickBubble();
            int value = Mathf.RoundToInt(data.bubbleValue * Mathf.Pow(seaweedLevel, data.costMultiplier));
            GameController.ResourcesManager.AddBubbles(value.ToString());
            Destroy(gameObject);
        }
    }
}