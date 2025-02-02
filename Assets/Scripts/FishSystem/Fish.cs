using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BubbleIdle.FishSystem
{
    public class Fish : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public FishData data { get; private set; }
        private SpriteRenderer sr;
        Vector3 targetPosition;
        private bool isDrag;

        private Tween currentTween;

        public virtual void Initialize(FishData data)
        {
            this.data = data;
            sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
            sr.sprite = data.fishIcon;
            
            GameController.ResourcesManager.AddProductionBonus(data.productionMultiplier);
            
            PlayTween();
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            currentTween.Kill();
            isDrag = true;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            transform.position += (Vector3)eventData.delta * 0.0185f;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            PlayTween();
            isDrag = false;
        }

        public void Refresh()
        {
            if (!isDrag)
            {
                if (Vector3.Distance(transform.position, targetPosition) <= 3)
                {
                    SetNewTargetPosition();
                    PlayTween();
                }
            
                UpdateRotation();
            }
        }

        private void UpdateRotation()
        {
            Vector3 direction = targetPosition - transform.position;

            transform.LookAt(direction);
        }

        private void PlayTween()
        {
            currentTween.Kill();
            currentTween = transform.DOMove(targetPosition, data.speed * 0.5f).SetEase(Ease.OutQuad);
        }

        private void SetNewTargetPosition()
        {
            while (Vector3.Distance(transform.position, targetPosition) <= 5)
            {
                targetPosition = new Vector3(
                    Random.Range(-data.movementRange.x, data.movementRange.x), 
                    Random.Range(-data.movementRange.y, data.movementRange.y), 
                    transform.position.z);
            }
        }
    }
}
