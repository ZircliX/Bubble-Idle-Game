using System;
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
        public event Action OnSeaweedBuy;
        public void BuySeaweed() => OnSeaweedBuy?.Invoke();
        public event Action OnSeaweedUpgrade;
        public void UpgradeSeaweed() => OnSeaweedUpgrade?.Invoke();
        
        //Money
        public event Action<int> OnMoneyAdd;
        public event Action OnMoneyAddSound;

        public void AddMoney(int value)
        {
            OnMoneyAdd?.Invoke(value);
            OnMoneyAddSound?.Invoke();
        }
    }
}