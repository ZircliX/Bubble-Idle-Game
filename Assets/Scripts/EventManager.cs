using System;
using BubbleIdle.SeaweedSystem;
using LTX.Singletons;

namespace BubbleIdle
{
    public class EventManager : MonoSingleton<EventManager>
    {
        //Bubbles
        public event Action OnBubbleSpawn;
        public void SpawnBubble() => OnBubbleSpawn?.Invoke();
        public event Action OnBubbleClick;
        public void ClickBubble() => OnBubbleClick?.Invoke();
        
        //Seaweeds
        public event Action<int> OnSeaweedBuy;
        public event Action OnSeaweedBuySound;

        public void BuySeaweed(int seaweedIndex)
        {
            OnSeaweedBuy?.Invoke(seaweedIndex);
            OnSeaweedBuySound?.Invoke();
        }

        public event Action OnSeaweedUpgrade;
        public void UpgradeSeaweed() => OnSeaweedUpgrade?.Invoke();
        
        //Money
        public event Action OnMoneyChange;

        public void ChangeMoney()
        {
            OnMoneyChange?.Invoke();
        }
    }
}