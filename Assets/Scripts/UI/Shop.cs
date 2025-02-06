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

        public void OpenUI()
        {
            EventManager.Instance.OpenUI();
        }
        
        private void UpdateUI()
        {
            //Seaweeds
            for (int i = 0; i < tabs.Length; i++)
            {
                UITabShop tab = tabs[i];

                if (SeaweedManager.Instance.seaweedDatas.Length > i)
                {
                    SeaweedData seaweed = SeaweedManager.Instance.seaweedDatas[i];
                    
                    //if already bought
                    if (SeaweedManager.Instance.seaweeds.ContainsKey(i))
                    {
                        int spriteIndex = Mathf.Clamp(SeaweedManager.Instance.seaweeds[i].currentLevel / 10, 0, 2);
                        tab.icon.sprite = seaweed.levelsIcon[spriteIndex];
                        tab.level.text = $"lvl. {SeaweedManager.Instance.seaweeds[i].currentLevel.ToString()}";
                        if (SeaweedManager.Instance.seaweeds[i].currentLevel == 20)
                        {
                            tab.cost.text = "MAX";
                            tab.cost.color = Color.black;
                            Destroy(tab.bubble);
                        }
                        else
                        {
                            tab.cost.color = Color.black;
                            if (SeaweedManager.Instance.seaweeds[i].GetUpgradeCost() <= GameController.ResourcesManager.BubbleCount)
                                tab.cost.color = Color.black;
                            else
                                tab.cost.color = Color.red;
                            
                            tab.cost.text = StaticTools.FormatNumber(SeaweedManager.Instance.seaweeds[i].GetUpgradeCost());
                        }
                    }
                    //if default UI
                    else
                    {
                        if (SeaweedManager.Instance.GetDefaultCost(i) <= GameController.ResourcesManager.BubbleCount)
                            tab.cost.color = Color.black;
                        else
                            tab.cost.color = Color.red;
                        
                        tab.icon.sprite = seaweed.levelsIcon[0];
                        tab.level.text = "lvl. 0";
                        tab.cost.text = StaticTools.FormatNumber(seaweed.baseCost);
                        tab.name.text = seaweed.seaweedName;
                    }
                }
            }

            //Fishs
            FishData fishData = FishManager.Instance.fishData;
            fishTab.cost.text = StaticTools.FormatNumber(Mathf.RoundToInt(fishData.baseCost * Mathf.Pow(FishManager.Instance.activeFishes.Count + 1, fishData.costMultiplier)));
            fishTab.cost.color = GameController.ResourcesManager.BubbleCount >= Mathf.RoundToInt(fishData.baseCost * Mathf.Pow(FishManager.Instance.activeFishes.Count + 1, fishData.costMultiplier))
                ? Color.black
                : Color.red;
        }

        private bool IsRich(int cost) => cost <= GameController.ResourcesManager.BubbleCount;
        public void UpgradeSeaweed(int index)
        {
            //Upgrade
            if (SeaweedManager.Instance.seaweeds.ContainsKey(index))
            {
                if (!IsRich(SeaweedManager.Instance.seaweeds[index].GetUpgradeCost())) return;
                
                if (SeaweedManager.Instance.seaweeds[index].currentLevel == 20) return;
                GameController.ResourcesManager.SpendBubbles(SeaweedManager.Instance.seaweeds[index].GetUpgradeCost());
                SeaweedManager.Instance.seaweeds[index].Upgrade();
                EventManager.Instance.UpgradeSeaweed();
            }
            //Buy
            else
            {
                if (!IsRich(SeaweedManager.Instance.GetDefaultCost(index))) return;
                
                GameController.ResourcesManager.SpendBubbles(SeaweedManager.Instance.GetDefaultCost(index));
                SeaweedManager.Instance.AddSeaweed(index);
                EventManager.Instance.BuySeaweed(index);
            }
        
            UpdateUI();
        }
        
        public void BuyFish()
        {
            FishData fishData = FishManager.Instance.fishData;
            int cost = Mathf.RoundToInt(fishData.baseCost *
                                        Mathf.Pow(FishManager.Instance.activeFishes.Count + 1,
                                            fishData.costMultiplier));
            
            if (IsRich(cost))
            {
                GameController.ResourcesManager.SpendBubbles(cost);
                GameController.ProgressionManager.totalFishes++;
                FishManager.Instance.SpawnFish();
            }
            
            UpdateUI();
        }
    }
}
