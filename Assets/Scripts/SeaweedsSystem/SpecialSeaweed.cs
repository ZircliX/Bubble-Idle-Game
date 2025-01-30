using UnityEngine;

namespace BubbleIdle.SeaweedSystem
{
    public class SpecialSeaweed : Seaweed
    {
        public override void Initialize(SeaweedData data, int level = 0)
        {
            this.data = data;
            this.currentLevel = level;
            
            sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
            sr.sprite = data.levelsIcon[level];
            
            ApplyBoostEffect();
        }

        public override void Refresh()
        {
            
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