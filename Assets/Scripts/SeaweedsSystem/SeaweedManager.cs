using System.Collections.Generic;
using BubbleIdle.SaveSystem;
using LTX.Singletons;
using UnityEngine;

namespace BubbleIdle.SeaweedSystem
{
    public class SeaweedManager : MonoSingleton<SeaweedManager>
    {
        [field : SerializeField] public Seaweed seaweedPrefab { get; private set; }
        [field : SerializeField] public SpecialSeaweed specialSeaweedPrefab { get; private set; }
        [field : SerializeField] public SeaweedData[] seaweedDatas { get; private set; }
        [field : SerializeField] public Transform[] seaweedsPos { get; private set; }
        public List<Seaweed> seaweeds { get; private set; } = new List<Seaweed>();
        
        protected override void Awake()
        {
            base.Awake();
            Debug.Log($"Time since last login : {Mathf.FloorToInt(GameController.ProgressionManager.SecondsPassed)} seconds");
            
            SpawnSavedSeaweeds();
        }

        private void Update()
        {
            foreach (Seaweed seaweed in seaweeds)
            {
                seaweed.Refresh();
            }
        }
        
        public void AddSeaweed(int seaweedIndex)
        {
            if (seaweedIndex == 3)
            {
                SpecialSeaweed newSpecialSeaweed = Instantiate(specialSeaweedPrefab);
                newSpecialSeaweed.Initialize(seaweedDatas[seaweedIndex], 1);
                seaweeds.Add(newSpecialSeaweed);
                newSpecialSeaweed.transform.position = seaweedsPos[newSpecialSeaweed.data.seaweedType].position;
                
                GameController.ProgressionManager.AddSeaweed(newSpecialSeaweed);
            }
            else
            {
                Seaweed newSeaweed = Instantiate(seaweedPrefab);
                newSeaweed.Initialize(seaweedDatas[seaweedIndex], 1);
                seaweeds.Add(newSeaweed);
                newSeaweed.transform.position = seaweedsPos[newSeaweed.data.seaweedType].position;
                
                GameController.ProgressionManager.AddSeaweed(newSeaweed);
            }
        }

        private void SpawnSavedSeaweeds()
        {
            foreach (SeaweedSave seaweedSave in GameController.ProgressionManager.seaweeds)
            {
                //Take saved seaweeds
                Seaweed newSeaweed = Instantiate(seaweedPrefab, seaweedSave.seaweedPosition, Quaternion.identity);
                seaweeds.Add(newSeaweed);
                newSeaweed.Initialize(seaweedSave.seaweedData, seaweedSave.seaweedLevel);
                EventManager.Instance.BuySeaweed(newSeaweed.data.seaweedType);
            }
        }
    }
}
