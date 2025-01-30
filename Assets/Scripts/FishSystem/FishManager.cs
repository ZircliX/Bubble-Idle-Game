using System.Collections.Generic;
using LTX.Singletons;
using UnityEngine;

namespace BubbleIdle.FishSystem
{
    public class FishManager : MonoSingleton<FishManager>
    {
        [SerializeField] private Fish fishPrefab;
        [field : SerializeField] public FishData fishData { get; private set; }
        public List<Fish> activeFishes { get; private set; }= new List<Fish>();

        public void SpawnFish()
        {
            Vector3 position = Vector3.zero;
            Fish newFish = Instantiate(fishPrefab, position, Quaternion.identity);
            newFish.Initialize(fishData);
            activeFishes.Add(newFish);
        }

        private void Update()
        {
            foreach (Fish fish in activeFishes)
            {
                fish.Refresh();
            }
        }
    }
    
}
