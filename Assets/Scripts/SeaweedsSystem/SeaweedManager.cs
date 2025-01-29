using System.Collections.Generic;
using BubbleIdle.Core.SeaweedsSystem;
using BubbleIdle.SaveSystem;
using BubbleIdle.SeaweedSystem;
using LTX.Singletons;
using UnityEngine;

namespace BubbleIdle.Core
{
    public class SeaweedManager : MonoSingleton<SeaweedManager>
    {
        [SerializeField] private Seaweed seaweedPrefab;
        [field : SerializeField] public Transform[] seaweedsPos { get; private set; }
        private List<Seaweed> seaweeds = new List<Seaweed>();
        
        protected override void Awake()
        {
            base.Awake();
            Debug.Log($"Time since last login : {Mathf.FloorToInt(GameController.ProgressionManager.SecondsPassed)} seconds");
            
            CalculateOfflineBubbleProduction();
        }

        private void Update()
        {
            foreach (Seaweed seaweed in seaweeds)
            {
                seaweed.Refresh();
            }
        }
        
        public void AddSeaweed(Seaweed newSeaweed)
        {
            seaweeds.Add(newSeaweed);
            newSeaweed.transform.position = seaweedsPos[newSeaweed.data.seaweedType].position;
            GameController.ProgressionManager.AddSeaweed(newSeaweed);
        }

        private void CalculateOfflineBubbleProduction()
        {
            foreach (SeaweedSave seaweedSave in GameController.ProgressionManager.seaweeds)
            {
                //Take saved seaweeds
                Seaweed newSeaweed = Instantiate(seaweedPrefab, seaweedSave.seaweedPosition, Quaternion.identity);
                seaweeds.Add(newSeaweed);
                newSeaweed.Initialize(seaweedSave.seaweedData, seaweedSave.seaweedLevel);
                
                //Calculate offline production
                float bubbleProductionRate = newSeaweed.GetProductionAtLevel() / newSeaweed.data.productionCooldown;
                int bubblesProduced = Mathf.FloorToInt(bubbleProductionRate * GameController.ProgressionManager.SecondsPassed);
                GameController.ResourcesManager.AddBubbles(bubblesProduced);
                Debug.Log($"Seaweed {seaweedSave.seaweedData.seaweedType} produced {bubblesProduced} bubbles while offline.");
            }
        }
    }
}
