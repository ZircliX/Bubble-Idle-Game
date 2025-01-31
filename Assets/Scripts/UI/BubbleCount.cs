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
            bubbles.text = FormatNumber(GameController.ResourcesManager.BubbleCount);
        }

        private string FormatNumber(BigInteger number)
        {
            if (number == 0) return "0";

            string[] suffixes = { "", "K", "M", "B", "T", "Q", "Qi", "Sx", "Sp", "Oc", "No", "De" };
            int suffixIndex = 0;
            BigInteger absNumber = BigInteger.Abs(number);

            // Find the appropriate suffix
            while (absNumber >= 1000 && suffixIndex < suffixes.Length - 1)
            {
                absNumber /= 1000;
                suffixIndex++;
            }

            // Calculate the shortened number
            BigInteger shortenedNumber = number / BigInteger.Pow(1000, suffixIndex);

            // Check if the number is a whole number after formatting
            bool isWholeNumber = (number % BigInteger.Pow(1000, suffixIndex)) == 0;

            // Format with 1 decimal if not whole, otherwise no decimals
            if (!isWholeNumber)
            {
                // Calculate the fractional part
                BigInteger fractionalPart = (number % BigInteger.Pow(1000, suffixIndex)) * 10 / BigInteger.Pow(1000, suffixIndex);
                return $"{shortenedNumber}.{fractionalPart}{suffixes[suffixIndex]}";
            }
            else
            {
                return $"{shortenedNumber}{suffixes[suffixIndex]}";
            }
        }
    }
}