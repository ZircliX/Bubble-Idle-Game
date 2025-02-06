using TMPro;
using UnityEngine;

namespace BubbleIdle.UI
{
    public class BubbleCount : MonoBehaviour
    {
        [SerializeField] private TMP_Text bubbles;
        [SerializeField] private TMP_Text specialBubbles;

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
            bubbles.text = StaticTools.FormatNumber(GameController.ResourcesManager.BubbleCount);
            specialBubbles.text = StaticTools.FormatNumber(GameController.ResourcesManager.SpecialBubbleCount);
        }
    }
}