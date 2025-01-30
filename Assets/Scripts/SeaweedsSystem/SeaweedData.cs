using BubbleIdle.BubbleSystem;
using UnityEngine;

namespace BubbleIdle.SeaweedSystem
{
    [CreateAssetMenu(menuName = "BubbleIdle/SeaweedData", fileName = "SeaweedData")]
    public class SeaweedData : ScriptableObject
    {
        [Header("Basic Infos")]
        public string seaweedName;
        public int seaweedType;
        public Sprite[] levelsIcon;

        [Header("Production")]
        public float baseProduction;
        public float productionCooldown;
        public float speedMultiplier;

        [Header("Upgrades")] 
        public int baseCost;
        public float costMultiplier;

        [Header("bubble Infos")]
        public Bubble bubblePrefab;
        public int bubbleValue;
        public float bubbleProductionRate;
    }
}