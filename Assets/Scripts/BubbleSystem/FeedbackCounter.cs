using DG.Tweening;
using LTX.Singletons;
using TMPro;
using UnityEngine;

namespace BubbleIdle.BubbleSystem
{
    public class FeedbackCounter : MonoSingleton<FeedbackCounter>
    {
        [SerializeField] private TMP_Text counterPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private float animeTime;

        public void SpawnCounter(Vector3 position, int amount)
        {
            TMP_Text spawnedCounter = Instantiate(counterPrefab, position, Quaternion.identity);
            Transform ct = spawnedCounter.transform;
            
            ct.SetParent(parent);
            ct.localScale = Vector3.one;
            ct.position = new Vector3(ct.position.x, ct.position.y, 0);
            
            spawnedCounter.text = $"+ {amount}";

            Color targetColor = new Color(255, 255, 255, 0);
            spawnedCounter.DOColor(targetColor, animeTime);
            spawnedCounter.transform.DOMoveY(spawnedCounter.transform.position.y + 5f, animeTime);
            Destroy(spawnedCounter.gameObject, animeTime);
        }
    }
}