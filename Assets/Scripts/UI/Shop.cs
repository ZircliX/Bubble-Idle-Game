using BubbleIdle.SeaweedSystem;
using UnityEngine;

namespace BubbleIdle.UI
{
    public class Shop : MonoBehaviour
    {
        [Header("UI Tabs")] 
        [SerializeField] private UITabShop[] tabs;

        private void OnEnable()
        {
            EventManager.Instance.OnMoneyChange += UpdateUI;
            UpdateUI();
        }

        private void OnDisable()
        {
            EventManager.Instance.OnMoneyChange -= UpdateUI;
        }
        
        private void UpdateUI()
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                UITabShop tab = tabs[i];
                tab.cost.color = GameController.ResourcesManager.BubbleCount >= int.Parse(tab.cost.text)
                    ? Color.black
                    : Color.red;

                if (SeaweedManager.Instance.seaweedDatas.Length > i)
                {
                    SeaweedData seaweed = SeaweedManager.Instance.seaweedDatas[i];
                    
                    //if already bought
                    if (SeaweedManager.Instance.seaweeds.Count > i)
                    {
                        tab.icon.sprite = seaweed.levelsIcon[SeaweedManager.Instance.seaweeds[i].currentLevel / 10];
                        tab.level.text = SeaweedManager.Instance.seaweeds[i].currentLevel.ToString();
                        tab.cost.text = SeaweedManager.Instance.seaweeds[i].GetUpgradeCost().ToString();
                    }
                    //if default UI
                    else
                    {
                        tab.icon.sprite = seaweed.levelsIcon[0];
                        tab.level.text = "0";
                        tab.cost.text = seaweed.baseCost.ToString();
                        tab.name.text = seaweed.seaweedName;
                    }
                }
            }
        }

        private bool IsRich(int index) => GameController.ResourcesManager.SpendBubbles(int.Parse(tabs[index].cost.text));
        public void UpgradeSeaweed(int index)
        {
            if (IsRich(index))
            {
                //Upgrade
                if (SeaweedManager.Instance.seaweeds.Count > index)
                {
                    SeaweedManager.Instance.seaweeds[index].Upgrade();
                    EventManager.Instance.UpgradeSeaweed();
                }
                //Buy
                else
                {
                    SeaweedManager.Instance.AddSeaweed(index);
                    EventManager.Instance.BuySeaweed(index);
                }
            }
            else
            {
                Debug.LogWarning("Not enough Money");
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
