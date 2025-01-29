using System;
using LTX.Singletons;
using UnityEngine;

namespace BubbleIdle.Core
{
    public class ResourcesManager : MonoSingleton<ResourcesManager>
    {
        public int BubbleCount { get; private set; }
        public event Action<int> OnBubbleCountChanged;
        public void AddBubbles(int amount)
        {
            BubbleCount += amount;
            OnBubbleCountChanged?.Invoke(BubbleCount);
            Debug.Log($"Bubbles Added:{amount}. Total : {BubbleCount}");
        }

        public bool SpendBubbles(int amount)
        {
            if (BubbleCount >= amount)
            {
                BubbleCount -= amount;
                OnBubbleCountChanged?.Invoke(BubbleCount);
                Debug.Log($"Bubbles Spent:{amount}. Remaining : {BubbleCount}");
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