using UnityEngine;

namespace BubbleIdle.FishSystem
{
    [CreateAssetMenu(menuName = "BubbleIdle/FishData", fileName = "FishData")]
    public class FishData : ScriptableObject
    {
        [Header("Basic Infos")]
        public string fishName;
        public Sprite fishIcon;
        
        [Header("Effect on Seaweed Production")]
        public float productionMultiplier;
        
        [Header("Cost & Upgrades")]
        public int baseCost;
        public float costMultiplier;
        
        [Header("Movement & Behavior")]
        public float speed;
        public float movementRange;
    }
}
