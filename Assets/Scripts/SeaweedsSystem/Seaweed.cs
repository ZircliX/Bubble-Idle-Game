using UnityEngine;

namespace BubbleIdle.SeaweedSystem
{
    public class Seaweed : MonoBehaviour
    {
        public SeaweedData data { get; private set; }
        public int currentLevel { get; private set; }
        public float currentProductionRate => 
            data.productionSpeed * 
            Mathf.Pow(data.speedMultiplier, currentLevel - 1);

        public virtual void Initialize(SeaweedData data, int level = 0)
        {
            this.data = data;
            this.currentLevel = level;
        }

        public virtual void Refresh()
        {
            Debug.Log("Refresh Seaweed");
        }

        public virtual void Upgrade()
        {
            currentLevel++;
        }
        
        public int GetUpgradeCost()
        {
            return Mathf.RoundToInt(
                data.baseUpgradeCost * 
                Mathf.Pow(data.costMultiplier, currentLevel - 1)
                );
        }
    }
}
