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
        public Dictionary<int, Seaweed> seaweeds { get; private set; } = new Dictionary<int, Seaweed>();
        
        protected override void Awake()
        {
            base.Awake();
            Debug.Log($"Time since last login : {GameController.ProgressionManager.SecondsPassed} seconds");
            
            SpawnSavedSeaweeds();
        }

        private void Update()
        {
            foreach (KeyValuePair<int, Seaweed> seaweed in seaweeds)
            {
                seaweed.Value.Refresh();
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
            seaweeds.Add(seaweedIndex, newSeaweed);
            newSeaweed.transform.position = seaweedsPos[newSeaweed.data.seaweedType].position;
            newSeaweed.transform.SetParent(seaweedsPos[newSeaweed.data.seaweedType]);
            newSeaweed.transform.localScale = Vector3.one;
            
            GameController.ProgressionManager.AddSeaweed(newSeaweed);
        }

        private void SpawnSavedSeaweeds()
        {
            GameController.ProgressionManager.LoadSeaweeds();
            
            foreach (SeaweedSave seaweedSave in GameController.ProgressionManager.seaweeds)
            {
                Seaweed newSeaweed;
                if (seaweedSave.typeIndex == 3)
                {
                    newSeaweed = Instantiate(specialSeaweedPrefab, seaweedSave.seaweedPosition, Quaternion.identity);
                }
                else
                {
                    newSeaweed = Instantiate(seaweedPrefab, seaweedSave.seaweedPosition, Quaternion.identity);
                }
                
                newSeaweed.transform.SetParent(seaweedsPos[seaweedSave.typeIndex]);
                newSeaweed.transform.localScale = Vector3.one;
                seaweeds.Add(seaweedSave.typeIndex, newSeaweed);
                newSeaweed.Initialize(seaweedDatas[seaweedSave.typeIndex], seaweedSave.seaweedLevel);
            }
        }
    }
}
