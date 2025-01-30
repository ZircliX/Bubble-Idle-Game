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
        
        public void Write(ref SaveFile saveFile)
        {
            DateTime time = DateTime.Now;
            string timeString = time.ToString("o"); // ISO 8601 format
            saveFile.quitTime = timeString;

            for (int index = 0; index < seaweeds.Count; index++)
            {
                SeaweedSave seaweedSave = seaweeds[index];
                seaweedSave.seaweedLevel = SeaweedManager.Instance.seaweeds[index].currentLevel;
            }

            saveFile.productionBonus = GameController.ResourcesManager.ProductionBonus;

            saveFile.seaweeds = seaweeds;
            saveFile.bubbles = GameController.ResourcesManager.BubbleCount.ToString();
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

            //Calculate offline production
            foreach (SeaweedSave newSeaweed in seaweeds)
            {
                float bubbleProductionRate = newSeaweed.seaweedData.baseProduction * Mathf.Pow(newSeaweed.seaweedLevel, newSeaweed.seaweedData.speedMultiplier);
                bubbleProductionRate /= newSeaweed.seaweedData.productionCooldown;
                double bubblesProduced = bubbleProductionRate * GameController.ProgressionManager.SecondsPassed;
                int bubblesProducedRounded = Mathf.RoundToInt((float)bubblesProduced * saveFile.productionBonus);
                GameController.ResourcesManager.AddBubbles(bubblesProducedRounded.ToString());
                //Debug.Log($"Seaweed {seaweedSave.seaweedData.seaweedType} produced {bubblesProduced} bubbles while offline.");
            }
        }

        public void AddSeaweed(Seaweed seaweed)
        {
            seaweeds.Add(new SeaweedSave
            {
                seaweedData = seaweed.data,
                seaweedLevel = seaweed.currentLevel,
                seaweedPosition = seaweed.transform.position,
            });
        }
    }
}