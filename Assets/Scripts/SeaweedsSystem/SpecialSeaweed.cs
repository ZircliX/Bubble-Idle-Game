using BubbleIdle.SeaweedSystem;
using log4net.Core;
using UnityEngine;
using static BubbleIdle.GameController;

namespace BubbleIdle.SeaweedSystem
{
    public class SpecialSeaweed : Seaweed
    {
        public SeaweedData data { get; private set; }
        public int currentLevel { get; private set; }
        private float productionTimer, bubbleTimer;
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