using UnityEngine;

namespace BubbleIdle.SeaweedSystem
{
    public class Seaweed : MonoBehaviour
    {
        public float productionSpeed = 1;
        public float speedMultiplier;
        public string seaweedName = "Seaweed";
        public int currentLevel = 1;


        public virtual void Initialize(SeaweedData data)
        {
            productionSpeed = data.productionSpeed;
            speedMultiplier = data.speedMultiplier;
        }

        public virtual void Refresh()
        {
            Debug.Log("Refresh Seaweed");
        }
    }
}
