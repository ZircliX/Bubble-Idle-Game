using BubbleIdle.SeaweedSystem;
using DG.Tweening;
using LTX.Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BubbleIdle.Core.UI
{
    public class MenuManager : MonoSingleton<MenuManager>
    {
        [SerializeField] private SeaweedInfo[] seaweedsInfos;
        private Tween shakeTween;

        private void OnEnable()
        {
            EventManager.Instance.OnSeaweedBuySound += UpdateUI;
            EventManager.Instance.OnSeaweedUpgrade += UpdateUI;
        }
        
        private void OnDisable()
        {
            EventManager.Instance.OnSeaweedBuySound -= UpdateUI;
            EventManager.Instance.OnSeaweedUpgrade -= UpdateUI;
        }
        
        public void OpenSeaweedPanel(SeaweedData data)
        {
            SeaweedInfo info = seaweedsInfos[data.seaweedType];
            for (int i = 0; i < seaweedsInfos.Length; i++)
            {
                if (seaweedsInfos[i].parent == null) continue;
                
                if (i == data.seaweedType)
                {
                    info.parent.SetActive(!info.parent.activeSelf);
                    continue;
                }
                
                seaweedsInfos[i].parent.SetActive(false);
            }

            if (shakeTween != null && shakeTween.IsActive())
            {
                shakeTween.Kill();
                info.parent.transform.localScale = Vector3.one;
            }

            // Apply a new shake effect
            shakeTween = info.parent.transform.DOShakeScale(0.1f, 0.2f)
                .OnComplete(() => shakeTween = null); // Clear the tween reference when done
            
            UpdateUI();
        }

        public void UpdateUI()
        {
            for (int i = 0; i < seaweedsInfos.Length; i++)
            {
                if (!SeaweedManager.Instance.seaweeds.TryGetValue(i, out Seaweed seaweed)) continue;
                
                SeaweedInfo info = seaweedsInfos[i];
                
                int prod = SeaweedManager.Instance.seaweeds[seaweed.data.seaweedType].GetProductionAtLevel();
                info.progressBar.fillAmount = seaweed.currentLevel / 20f;
                info.level.text = $"lvl. {seaweed.currentLevel.ToString()}";
                info.prodPerSecond.text = $"+{StaticTools.FormatNumber(prod / seaweed.data.productionCooldown)} /s";

                info.parent.transform.DOShakePosition(0.2f, 0.3f);
            }
        }
    }

    [System.Serializable]
    public struct SeaweedInfo
    {
        public GameObject parent;
        public Image progressBar;
        
        public TMP_Text level;
        public TMP_Text prodPerSecond;
    }
}