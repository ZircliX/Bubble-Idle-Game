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
        
        protected override void Awake()
        {
            base.Awake();
            Debug.Log($"Time since last login : {GameController.ProgressionManager.SecondsPassed} seconds");

            GameController.OnGameLoad += () =>
            {
                for (int i = 0; i < GameController.ProgressionManager.totalFishes; i++)
                {
                    SpawnFish();
                }
            };
        }
        
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
