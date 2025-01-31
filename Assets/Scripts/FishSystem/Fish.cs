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
            transform.DOMove(targetPosition, data.speed * Time.deltaTime).SetEase(Ease.InCubic);
            
            if (Vector3.Distance(transform.position, targetPosition) <= 2)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, data.speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) <= 1)
            {
                SetNewTargetPosition();
            }

            UpdateRotation();
        }
        
        private void UpdateRotation()
        {
            Vector3 direction = targetPosition - transform.position;

            transform.LookAt(direction);
            //transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
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
