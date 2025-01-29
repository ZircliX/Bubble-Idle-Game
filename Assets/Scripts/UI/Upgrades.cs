using System;
using System.Text.RegularExpressions;
using BubbleIdle.SeaweedSystem;
using UnityEngine;

namespace BubbleIdle.UI
{
    public class Upgrades : MonoBehaviour
    {
        [Header("UI Tabs")] 
        [SerializeField] private UITabUpgrade[] tabs;
        
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

        public void UpgradeSeaweed(int index)
        {
            if (IsRich(index))
            {
                SeaweedManager.Instance.seaweeds[index].Upgrade();
                
                Match match = Regex.Match(tabs[index].level.text, @"\d+");

                if (match.Success)
                {
                    int level = int.Parse(match.Value);
                    Console.WriteLine(level); // Output: 2
                    tabs[index].level.text = $"lvl. {level + 1}";
                }
                
                tabs[index].cost.text = SeaweedManager.Instance.seaweeds[index].GetUpgradeCost().ToString();
            }
        }
    }
}