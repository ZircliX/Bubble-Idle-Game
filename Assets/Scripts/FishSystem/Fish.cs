using UnityEngine;

namespace BubbleIdle.FishSystem
{
    public class Fish : MonoBehaviour
    {
        public FishData data { get; private set; }
        private SpriteRenderer sr;
        Vector3 targetPosition = default;

        public virtual void Initialize(FishData data)
        {
            this.data = data;
            sr = GetComponent<SpriteRenderer>();
            sr.sprite = data.fishIcon;
            GameController.ResourcesManager.AddProductionBonus(data.productionMultiplier);
        }

        public void Refresh()
        {
            Vector3 direction = targetPosition - transform.position;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, data.speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) <= 1)
            {
                SetNewTargetPosition();
            }
        }

        private void SetNewTargetPosition()
        {
            targetPosition = new Vector3(Random.Range(-data.movementRange, data.movementRange), Random.Range(-data.movementRange, data.movementRange), transform.position.z);
        }
    }
}
