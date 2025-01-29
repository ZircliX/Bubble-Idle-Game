using BubbleIdle.BubbleSystem;
using UnityEngine;

namespace BubbleIdle.SeaweedSystem
{
    public class Seaweed : MonoBehaviour
    {
        public SeaweedData data { get; private set; }
        public int currentLevel { get; private set; }
        private float productionTimer;
        private SpriteRenderer sr;

        public virtual void Initialize(SeaweedData data, int level = 0)
        {
            this.data = data;
            this.currentLevel = level;

            sr = GetComponent<SpriteRenderer>();
            sr.sprite = data.levelsIcon[0];
        }

        public virtual void Refresh()
        {
            productionTimer += Time.deltaTime;
            if (productionTimer >= data.productionCooldown)
            {
                ProduceBubble();
                productionTimer = 0;
            }
        }

        private void ProduceBubble()
        {
            Bubble newBubble = Instantiate(data.bubblePrefab);
            newBubble.transform.position = transform.position;
            newBubble.Initialize(data.bubbleValue);
        }

        public virtual void Upgrade()
        {
            currentLevel++;
        }
        
        public int GetUpgradeCost(int nextLevel = 0)
        {
            float nextLevelCost = data.baseCost * Mathf.Pow(currentLevel + nextLevel, 1.5f);
            return Mathf.RoundToInt(nextLevelCost);
        }

        public float GetCurrentProductionRate(int nextLevel = 0)
        {
            float nextLevelProduction = data.baseProduction * Mathf.Pow(currentLevel + nextLevel, 1.2f);
            return nextLevelProduction / data.productionCooldown;
        }
    }
}
