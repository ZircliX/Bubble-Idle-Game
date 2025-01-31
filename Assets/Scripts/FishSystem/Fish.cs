using DG.Tweening;
using UnityEngine;

namespace BubbleIdle.FishSystem
{
    public class Fish : MonoBehaviour
    {
        public FishData data { get; private set; }
        private SpriteRenderer sr;
        Vector3 targetPosition;

        public virtual void Initialize(FishData data)
        {
            this.data = data;
            sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
            sr.sprite = data.fishIcon;
            GameController.ResourcesManager.AddProductionBonus(data.productionMultiplier);
        }

        public void Refresh()
        {
            transform.DOMove(targetPosition, data.speed * 0.5f).SetEase(Ease.OutQuad);

            if (Vector3.Distance(transform.position, targetPosition) <= 3)
            {
                SetNewTargetPosition();
            }
            
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            Vector3 direction = targetPosition - transform.position;

            transform.LookAt(direction);
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
