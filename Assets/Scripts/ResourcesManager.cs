using System.Numerics;
using UnityEngine;

namespace BubbleIdle
{
    public class ResourcesManager
    {
        public float ProductionBonus { get; private set; } = 1;
        public BigInteger BubbleCount { get; private set; }
        
        public int SpecialBubbleCount { get; private set; }
        public int DriedCount { get; private set; }

        public void AddDried(int amount)
        {
            DriedCount += amount;
        }
        
        public void AddBubbles(string amount)
        {
            if (BigInteger.TryParse(amount, out BigInteger result))
            {
                AddBubbles(result);
            }
        }
        public void AddBubbles(BigInteger amount)
        {
            BubbleCount += BigInteger.Abs(amount);
            EventManager.Instance.ChangeMoney();
        }

        public void AddSpecialBubbles(int amount)
        {
            SpecialBubbleCount += amount;
            EventManager.Instance.ChangeMoney();
        }

        public void AddProductionBonus(float amount)
        {
            ProductionBonus += amount;
        }

        public void SpendBubbles(string amount)
        {
            SpendBubbles(BigInteger.Parse(amount));
        }
        public void SpendBubbles(BigInteger amount)
        {
            if (BubbleCount >= amount)
            {
                BubbleCount -= amount;
                EventManager.Instance.ChangeMoney();
            }
            else
            {
                Debug.LogWarning("Not enough bubbles noob!");
            }
        }
    }
}