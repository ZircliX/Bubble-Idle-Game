using System.Collections.Generic;
using BubbleIdle.SeaweedSystem;
using UnityEngine;

namespace BubbleIdle.UI
{
    public class Shop : MonoBehaviour
    {
        [Header("UI Tabs")] 
        [SerializeField] private UITabShop[] tabs;
        
        public Seaweed seaweedPrefab;
        public List<SeaweedData> seaweedTypes;

        private void OnEnable()
        {
            EventManager.Instance.OnMoneyChange += UpdateUI;
        }

        private void OnDisable()
        {
            EventManager.Instance.OnMoneyChange -= UpdateUI;
        }
        
        private void UpdateUI()
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                //Change text to red if too poor
                tabs[i].cost.color = GameController.ResourcesManager.BubbleCount >= int.Parse(tabs[i].cost.text)
                    ? Color.black
                    : Color.red;
            }
        }

        private bool IsRich(int index)
        {
            return GameController.ResourcesManager.SpendBubbles(int.Parse(tabs[index].cost.text));
        }

        public void BuySeaweed(int index)
        {
            if (IsRich(index))
            {
                Seaweed newSeaweed = Instantiate(seaweedPrefab);
                newSeaweed.Initialize(seaweedTypes[0], 1);
                
                SeaweedManager.Instance.AddSeaweed(newSeaweed);
                EventManager.Instance.BuySeaweed(newSeaweed);
            }
        }
        
        public void BuyFish(int index)
        {
            if (IsRich(index))
            {
                //Spawn Fish
            }
        }
        
        public void BuyCoral(int index)
        {
            if (IsRich(index))
            {
                //Add Coral
            }
        }
    }
}
