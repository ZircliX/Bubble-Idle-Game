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
            Debug.Log($"Time since last login : {GameController.ProgressionManager.SecondsPassed} seconds");
            
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
            Seaweed newSeaweed;
            if (seaweedIndex == 3)
            {
                newSeaweed = Instantiate(specialSeaweedPrefab);
            }
            else
            {
                newSeaweed = Instantiate(seaweedPrefab);
            }
            
            newSeaweed.Initialize(seaweedDatas[seaweedIndex], 1);
            seaweeds.Add(newSeaweed);
            newSeaweed.transform.position = seaweedsPos[newSeaweed.data.seaweedType].position;
            
            GameController.ProgressionManager.AddSeaweed(newSeaweed);
        }

        private void SpawnSavedSeaweeds()
        {
            foreach (SeaweedSave seaweedSave in GameController.ProgressionManager.seaweeds)
            {
                Seaweed newSeaweed;
                if (seaweedSave.seaweedData.seaweedType == 3)
                {
                    newSeaweed = Instantiate(specialSeaweedPrefab, seaweedSave.seaweedPosition, Quaternion.identity);
                }
                else
                {
                    newSeaweed = Instantiate(seaweedPrefab, seaweedSave.seaweedPosition, Quaternion.identity);
                    
                }
                //Take saved seaweeds
                seaweeds.Add(newSeaweed);
                newSeaweed.Initialize(seaweedSave.seaweedData, seaweedSave.seaweedLevel);
                EventManager.Instance.BuySeaweed(newSeaweed.data.seaweedType);
            }
        }
    }
}
