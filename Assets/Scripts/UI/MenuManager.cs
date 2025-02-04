using BubbleIdle.SeaweedSystem;
using LTX.Singletons;
using TMPro;
using UnityEngine;

namespace BubbleIdle.Core.UI
{
    public class MenuManager : MonoSingleton<MenuManager>
    {
        [SerializeField] private SeaweedInfo[] seaweedsInfos;
        private MenuState menuState;
        
        public void OpenSeaweedPanel(SeaweedData data)
        {
            SeaweedInfo info = seaweedsInfos[data.seaweedType];
            for (int i = 0; i < seaweedsInfos.Length; i++)
            {
                if (i == data.seaweedType)
                {
                    info.parent.SetActive(!info.parent.activeSelf);
                    continue;
                }

                seaweedsInfos[i].parent.SetActive(false);
            }
            
            UpdateUI();
        }

        public void UpdateUI()
        {
            for (int i = 0; i < seaweedsInfos.Length; i++)
            {
                if (!SeaweedManager.Instance.seaweeds.TryGetValue(i, out Seaweed seaweed)) continue;
                
                SeaweedInfo info = seaweedsInfos[i];
                
                int prod = SeaweedManager.Instance.seaweeds[seaweed.data.seaweedType].GetProductionAtLevel();
                info.name.text = seaweed.data.seaweedName;
                info.level.text = seaweed.currentLevel.ToString();
                info.prodPerSecond.text = $"+{StaticTools.FormatNumber(prod / seaweed.data.productionCooldown)} /s";
            }
        }
    }

    [System.Serializable]
    public enum MenuState
    {
        Gameplay,
        Seaweed,
    }

    [System.Serializable]
    public struct SeaweedInfo
    {
        public GameObject parent;
        
        public TMP_Text name;
        public TMP_Text level;
        public TMP_Text prodPerSecond;
    }
}