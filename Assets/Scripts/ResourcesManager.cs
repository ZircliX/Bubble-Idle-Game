using System;
using UnityEngine;

namespace BubbleIdle
{
    public class ResourcesManager
    {
        public float ProductionBonus { get; private set; }
        public long BubbleCount { get; private set; }
        
        public void AddBubbles(long amount)
        {
            BubbleCount += amount;
            EventManager.Instance.ChangeMoney();
            //Debug.Log($"Bubbles Added:{amount}. Total : {BubbleCount}");
        }

        public void AddProductionBonus(float amount)
        {
            ProductionBonus += amount;
        }

        public bool SpendBubbles(long amount)
        {
            Debug.Log($"Amount : {amount}");
            if (BubbleCount >= amount)
            {
                BubbleCount -= amount;
                EventManager.Instance.ChangeMoney();
                //Debug.Log($"Bubbles Spent : {amount} | Remaining : {BubbleCount}");
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