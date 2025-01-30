using System;
using System.Collections.Generic;
using LTX.Singletons;
using UnityEngine;

namespace BubbleIdle.FishSystem
{
    public class FishManager : MonoSingleton<FishManager>
    {
        private List<Fish> activeFishes = new List<Fish>();

        public Fish SpawnFish(FishData fishData, Vector3 position)
        {
            GameObject fishObject = new GameObject(fishData.fishName);
            Fish newFish = fishObject.AddComponent<Fish>();
            newFish.Initialize(fishData);
            newFish.transform.position = position;
            activeFishes.Add(newFish);
            return newFish;
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
