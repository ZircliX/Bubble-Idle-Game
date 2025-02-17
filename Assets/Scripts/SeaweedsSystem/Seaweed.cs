using BubbleIdle.BubbleSystem;
using UnityEngine;

namespace BubbleIdle.SeaweedSystem
{
    public class Seaweed : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer sr;
        public SeaweedData data { get; protected set; }
        public int currentLevel { get; protected set; }
        protected float productionTimer, bubbleTimer;

        public virtual void Initialize(SeaweedData data, int level = 0)
        {
            this.data = data;
            this.currentLevel = level;

            int spriteIndex = Mathf.Clamp(level / 10, 0, 2);
            sr.sprite = data.levelsIcon[spriteIndex];
        }

        public virtual void Refresh()
        {
            productionTimer += Time.deltaTime;
            if (productionTimer >= data.productionCooldown)
            {
                GameController.ResourcesManager.AddBubbles(GetProductionAtLevel().ToString());
                productionTimer = 0;
            }

            bubbleTimer += Time.deltaTime;
            if (bubbleTimer >= data.bubbleProductionRate)
            {
                ProduceBubble();
                bubbleTimer = 0;
            }
        }

        protected virtual void ProduceBubble()
        {
            Bubble newBubble = Instantiate(data.bubblePrefab);
            newBubble.transform.position = transform.position;
            newBubble.Initialize(data, currentLevel);
        }

        public virtual void Upgrade()
        {
            currentLevel++;
            int spriteIndex = Mathf.Clamp(currentLevel / 10, 0, 2);
            sr.sprite = data.levelsIcon[spriteIndex];
        }
        
        public int GetUpgradeCost(int nextLevel = 0)
        {
            float nextLevelCost = data.baseCost * Mathf.Pow(data.costMultiplier, currentLevel + nextLevel);
            return Mathf.RoundToInt(nextLevelCost);
        }

        public int GetProductionAtLevel(int nextLevel = 0)
        {
            float nextLevelProduction = data.baseProduction * Mathf.Pow(data.productionMultiplier, currentLevel + nextLevel);
            return Mathf.RoundToInt(nextLevelProduction * GameController.ResourcesManager.ProductionBonus);
        }
    }
}
