using UnityEngine;

namespace BubbleIdle.SeaweedSystem
{
    [CreateAssetMenu(menuName = "BubbleIdle/SeaweedData", fileName = "SeaweedData")]
    public class SeaweedData : ScriptableObject
    {
        [Header("Basic Infos")]
        public string seaweedName;
        public Sprite[] levelsIcon;
        
        [Header("Production")]
        public float productionSpeed;
        public float speedMultiplier;

        [Header("Upgrades")] 
        public int baseUpgradeCost;
        public float costMultiplier;
    }
}
