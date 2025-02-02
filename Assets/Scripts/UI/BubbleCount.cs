using System;
using System.Numerics;
using TMPro;
using UnityEngine;

namespace BubbleIdle.UI
{
    public class BubbleCount : MonoBehaviour
    {
        [SerializeField] private TMP_Text bubbles;

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
        }
    }
}