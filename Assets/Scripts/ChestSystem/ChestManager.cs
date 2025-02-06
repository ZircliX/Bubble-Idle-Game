using System.Collections;
using BubbleIdle.FishSystem;
using DG.Tweening;
using LTX.Singletons;
using UnityEngine;
using UnityEngine.UI;

namespace BubbleIdle.Chest
{
    public class ChestManager : MonoSingleton<ChestManager>
    {
        [SerializeField] private GameObject popUp;
        [SerializeField] private GameObject purchasePanel;
        [SerializeField] private GameObject notEnoughPanel;
        [SerializeField] private GameObject[] chests;
        [SerializeField] private Sprite[] gains;
        [SerializeField] private Image gain;
        [SerializeField] private Transform spawnPoint;
        private bool isChestOpened;

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
            if (GameController.ResourcesManager.SpecialBubbleCount >= 10) 
            {
                popUp.SetActive(true);
            }
            else
            {
                popUp.SetActive(false);
            }
        }

        public void OnChestClick()
        {
            if (GameController.ResourcesManager.SpecialBubbleCount >= 10)
            {
                for (int i = 0; i < chests.Length; i++)
                {
                    chests[i].SetActive(true);
                }
                GameController.ResourcesManager.SpendSpecialBubbles(10);
                purchasePanel.SetActive(true);
                purchasePanel.transform.DOShakeScale(0.2f, 0.2f);
            }
            else
            {
                GameObject info = Instantiate(notEnoughPanel, spawnPoint);
                info.transform.DOLocalMoveY(info.transform.localPosition.y + 50, 2f);
                Destroy(info, 2f);
            }
        }

        public void OpenChest(int typeIndex)
        {
            gain.gameObject.SetActive(true);
            
            switch (typeIndex)
            {
                case 0:
                    Debug.Log("Nothing");
                    break;
                case 1:
                    Debug.Log("Bubbles");
                    GameController.ResourcesManager.AddBubbles(GameController.ResourcesManager.BubbleCount / 2);
                    break;
                case 2:
                    Debug.Log("Fish");
                    FishManager.Instance.SpawnFish();
                    break;
                case 3:
                    Debug.Log("Fountain");
                    break;
            }

            for (int i = 0; i < chests.Length; i++)
            {
                chests[i].SetActive(false);
            }

            gain.sprite = gains[typeIndex];
            StartCoroutine(Hide());
        }

        private IEnumerator Hide()
        {
            yield return new WaitForSeconds(2f);
            purchasePanel.SetActive(false);
            gain.gameObject.SetActive(false);
        }
    }
}