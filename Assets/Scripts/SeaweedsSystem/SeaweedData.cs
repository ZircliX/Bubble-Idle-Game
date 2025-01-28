using UnityEngine;

namespace BubbleIdle.SeaweedSystem
{
    [CreateAssetMenu(menuName = "BubbleIdle/SeaweedData", fileName = "SeaweedData")]
    public class SeaweedData : ScriptableObject
    {
        public string seaweedName;
        public float productionSpeed;
        public float speedMultiplier;
        public Sprite[] levelsIcon;
    }
}
