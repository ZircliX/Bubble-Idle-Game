using System.Numerics;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace BubbleIdle
{
    public static class StaticTools
    {
        public static string FormatNumber(float number)
        {
            return FormatNumber(Mathf.RoundToInt(number));
        }
        
        public static string FormatNumber(int number)
        {
            return FormatNumber(BigInteger.Parse(number.ToString()));
        } 
        
        public static string FormatNumber(BigInteger number)
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
        
        public static void StartBubbleMovement(this Transform transform, Vector3 initialPosition, float moveDuration = 10, float swingAmplitude = 1, float swingSpeed = 30)
        {
            // Randomizing swing speed and amplitude for variation
            swingSpeed += Random.Range(-0.8f, 0.8f);
            swingAmplitude += Random.Range(-0.8f, 0.8f);
            moveDuration += Random.Range(-5f, 2f);
            
            transform.localScale = Vector3.one * 0.75f + Vector3.one * Random.Range(0.3f, 0.8f);
            
            // Mouvement vertical principal
            transform.DOMoveY(initialPosition.y + 20f, moveDuration)
                .SetEase(Ease.OutQuad);
            
            // Mouvement de swing horizontal
            int negativeSwing = Random.Range(0, 2) == 1 ? -1 : 1;
            DOTween.To(() => 0f, x => {
                    float swingOffset = Mathf.Sin(x * swingSpeed) * swingAmplitude;
                    transform.position = new Vector3(
                        initialPosition.x + swingOffset * negativeSwing,
                        transform.position.y,
                        transform.position.z
                    );
                }, 1f, moveDuration)
                .SetEase(Ease.Linear);
        }
    }
}