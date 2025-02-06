using BubbleIdle.BubbleSystem;
using UnityEngine;

namespace BubbleIdle.SeaweedSystem
{
    public class SpecialSeaweed : Seaweed
    {
        public override void Initialize(SeaweedData data, int level = 0)
        {
            this.data = data;
            this.currentLevel = level;
            
            int spriteIndex = Mathf.Clamp(level / 10, 0, 2);
            sr.sprite = data.levelsIcon[spriteIndex];

            for (int i = 0; i < level; i++)
            {
                ApplyBoostEffect();
            }
        }

        public override void Refresh()
        {
            bubbleTimer += Time.deltaTime;
            if (bubbleTimer >= data.bubbleProductionRate)
            {
                ProduceBubble();
                bubbleTimer = 0;
            }
        }

        protected override void ProduceBubble()
        {
            Bubble newBubble = Instantiate(data.bubblePrefab);
            newBubble.transform.position = transform.position;
            newBubble.Initialize(data, currentLevel);
        }

        public override void Upgrade()
        {
            currentLevel++;
            ApplyBoostEffect();
            int spriteIndex = Mathf.Clamp(currentLevel / 10, 0, 2);
            sr.sprite = data.levelsIcon[spriteIndex];
        }

        private void ApplyBoostEffect()
        {
            GameController.ResourcesManager.AddProductionBonus(data.baseProduction);
        }
    }
}