using System.Numerics;
using UnityEngine;

namespace BubbleIdle
{
    public class ResourcesManager
    {
        public float ProductionBonus { get; private set; } = 1;
        public BigInteger BubbleCount { get; private set; }
        
        public void AddBubbles(string amount)
        {
            if (BigInteger.TryParse(amount, out BigInteger result))
            {
                AddBubbles(result);
            }
        }
        public void AddBubbles(BigInteger amount)
        {
            BubbleCount += amount;
            EventManager.Instance.ChangeMoney();
        }

        public void AddProductionBonus(float amount)
        {
            ProductionBonus += amount;
        }

        public bool SpendBubbles(string amount)
        {
            if (BigInteger.TryParse(amount, out BigInteger result))
            {
                return SpendBubbles(result);
            }
            else
            {
                return false;
            }
        }
        public bool SpendBubbles(BigInteger amount)
        {
            if (BubbleCount >= amount)
            {
                BubbleCount -= amount;
                EventManager.Instance.ChangeMoney();
                return true;
            }
            else
            {
                Debug.LogWarning("Not enough bubbles noob!");
                return false;
            }
        }
    }
}