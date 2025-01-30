using UnityEngine;

namespace BubbleIdle.SeaweedSystem
{
    public class SpecialSeaweed : Seaweed
    {
        private SpriteRenderer sr;
        
        public override void Initialize(SeaweedData data, int level = 0)
        {
            this.data = data;
            this.currentLevel = level;
            
            sr = GetComponent<SpriteRenderer>();
            sr.sprite = data.levelsIcon[0];
            
            ApplyBoostEffect();
        }
        
        public override void Upgrade()
        {
            currentLevel++;
            ApplyBoostEffect();
            sr.sprite = data.levelsIcon[currentLevel / 10];
        }

        private void ApplyBoostEffect()
        {
            GameController.ResourcesManager.AddProductionBonus(data.baseProduction);
        }
    }
}