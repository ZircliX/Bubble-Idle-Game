using BubbleIdle.FishSystem;
using BubbleIdle.SeaweedSystem;
using UnityEngine;

namespace BubbleIdle.UI
{
    public class Shop : MonoBehaviour
    {
        [Header("UI Tabs")] 
        [SerializeField] private UITabShop[] tabs;
        [SerializeField] private UITabShop fishTab;

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
            //Seaweeds
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
                        int spriteIndex = Mathf.Clamp(SeaweedManager.Instance.seaweeds[i].currentLevel / 10, 0, 2);
                        tab.icon.sprite = seaweed.levelsIcon[spriteIndex];
                        tab.level.text = $"lvl. {SeaweedManager.Instance.seaweeds[i].currentLevel.ToString()}";
                        tab.cost.text = SeaweedManager.Instance.seaweeds[i].GetUpgradeCost().ToString();
                    }
                    //if default UI
                    else
                    {
                        tab.icon.sprite = seaweed.levelsIcon[0];
                        tab.level.text = "lvl. 0";
                        tab.cost.text = seaweed.baseCost.ToString();
                        tab.name.text = seaweed.seaweedName;
                    }
                }
            }

            //Fishs
            FishData fishData = FishManager.Instance.fishData;
            fishTab.cost.text = Mathf.RoundToInt(fishData.baseCost * Mathf.Pow(FishManager.Instance.activeFishes.Count + 1, fishData.costMultiplier)).ToString();
            fishTab.cost.color = GameController.ResourcesManager.BubbleCount >= int.Parse(fishTab.cost.text)
                ? Color.black
                : Color.red;
        }

        private bool IsRich(string cost) => GameController.ResourcesManager.SpendBubbles(long.Parse(cost));
        public void UpgradeSeaweed(int index)
        {
            if (IsRich(tabs[index].cost.text))
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
        
        public void BuyFish()
        {
            if (IsRich(fishTab.cost.text))
            {
                FishManager.Instance.SpawnFish();
                UpdateUI();
            }
        }
    }
}
