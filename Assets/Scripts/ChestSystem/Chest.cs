using DG.Tweening;
using UnityEngine;

namespace BubbleIdle.Chest
{
    public class Chest : MonoBehaviour
    {
        public GameObject popUp;
        [SerializeField] private GameObject purchasePanel;
        [SerializeField] private GameObject notEnoughPanel;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Animator chestAnimator;
        private bool isChestOpened = false;

        public void Update()
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
            if (isChestOpened)
            {
                return;
            }

            if (GameController.ResourcesManager.SpecialBubbleCount >= 10)
            {
                GameController.ResourcesManager.SpendSpecialBubbles(10);
                purchasePanel.SetActive(true);

                if (chestAnimator != null)
                {
                    chestAnimator.enabled = true;
                    chestAnimator.Play("ChestOpen");
                }

                DisableChest();
            }
            else
            {
                GameObject info = Instantiate(notEnoughPanel, spawnPoint);
                info.transform.DOLocalMoveY(info.transform.localPosition.y + 50, 2f);
                Destroy(info, 2f);
            }
        }

        private void DisableChest()
        {
            isChestOpened = true;
        }
    }
}