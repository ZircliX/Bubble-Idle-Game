using BubbleIdle.SeaweedSystem;
using DG.Tweening;
using LTX.Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            SceneManager.sceneLoaded += UpdateUIOnScene;
        }

        private void UpdateUIOnScene(Scene arg0, LoadSceneMode arg1)
        {
            UpdateUI();
        }

        private void OnDisable()
        {
            EventManager.Instance.OnSeaweedBuySound -= UpdateUI;
            EventManager.Instance.OnSeaweedUpgrade -= UpdateUI;
            SceneManager.sceneLoaded -= UpdateUIOnScene;
        }

        public void UpdateUI()
        {
            for (int i = 0; i < seaweedsInfos.Length; i++)
            {
                if (!SeaweedManager.Instance.seaweeds.TryGetValue(i, out Seaweed seaweed)) continue;
                
                SeaweedInfo info = seaweedsInfos[i];
                info.parent.SetActive(true);
                
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