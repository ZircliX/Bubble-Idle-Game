using System.Collections.Generic;
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

            foreach (SeaweedSave seaweedSave in GameController.ProgressionManager.seaweeds)
            {
                Seaweed newSeaweed = Instantiate(seaweedPrefab, seaweedSave.seaweedPosition, Quaternion.identity);
                seaweeds.Add(newSeaweed);
                newSeaweed.Initialize(seaweedSave.seaweedData, seaweedSave.seaweedLevel);
            }
            //GameController.ProgressionManager.SecondsPassed
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
    }
}
