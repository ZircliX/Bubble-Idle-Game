using System;
using System.Collections.Generic;
using BubbleIdle.SeaweedSystem;
using SaveSystem.Core;
using UnityEngine;

namespace BubbleIdle.SaveSystem
{
    public class ProgressionManager : ISaveListener<SaveFile>
    {
        public int Priority => 1;
        
        public List<SeaweedSave> seaweeds { get; private set; } = new  List<SeaweedSave>();
        public double SecondsPassed { get; private set; }
        public int totalFishes;

        private SaveFile saveFile;
        
        public void Write(ref SaveFile saveFile)
        {
            DateTime time = DateTime.Now;
            string timeString = time.ToString("o"); // ISO 8601 format
            saveFile.quitTime = timeString;

            for (int index = 0; index < seaweeds.Count; index++)
            {
                SeaweedSave seaweedSave = seaweeds[index];
                if (SeaweedManager.Instance.seaweeds.ContainsKey(index))
                {
                    seaweedSave.seaweedLevel = SeaweedManager.Instance.seaweeds[index].currentLevel;
                    seaweedSave.seaweedPosition = SeaweedManager.Instance.seaweeds[index].transform.position;
                }
            }

            saveFile.productionBonus = GameController.ResourcesManager.ProductionBonus;

            saveFile.seaweeds = seaweeds;
            saveFile.bubbles = GameController.ResourcesManager.BubbleCount.ToString();
            saveFile.totalFishes = totalFishes;
        }

        public void Read(in SaveFile saveFile)
        {
            seaweeds.Clear();

            DateTime savedTime = DateTime.Parse(saveFile.quitTime);
            DateTime currentTime = DateTime.Now;
            TimeSpan timePassed = currentTime - savedTime;
            SecondsPassed = timePassed.TotalSeconds;
            
            GameController.ResourcesManager.AddBubbles(saveFile.bubbles);
            seaweeds = saveFile.seaweeds;
            totalFishes = saveFile.totalFishes;

            //Calculate offline production
            this.saveFile = saveFile;
        }

        public void LoadSeaweeds()
        {
            foreach (SeaweedSave newSeaweed in seaweeds)
            {
                SeaweedData seaweedData = SeaweedManager.Instance.seaweedDatas[newSeaweed.typeIndex];
                
                float bubbleProductionRate = seaweedData.baseProduction * Mathf.Pow(newSeaweed.seaweedLevel, seaweedData.productionMultiplier);
                bubbleProductionRate /= seaweedData.productionCooldown;
                double bubblesProduced = bubbleProductionRate * GameController.ProgressionManager.SecondsPassed;
                int bubblesProducedRounded = Mathf.RoundToInt((float)bubblesProduced * saveFile.productionBonus) / 2;
                GameController.ResourcesManager.AddBubbles(bubblesProducedRounded.ToString());
                //Debug.Log($"Seaweed {seaweedSave.seaweedData.seaweedType} produced {bubblesProduced} bubbles while offline.");
            }
        }

        public void AddSeaweed(Seaweed seaweed)
        {
            seaweeds.Add(new SeaweedSave
            {
                typeIndex = seaweed.data.seaweedType,
                seaweedLevel = seaweed.currentLevel,
                seaweedPosition = seaweed.transform.position,
            });
        }
    }
}