using System;
using System.Collections.Generic;
using System.Numerics;
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
            string timeString = Now(); // ISO 8601 format
            //Debug.Log("TIME");
            //Debug.Log(DateTime.UtcNow);
            //Debug.Log(timeString);
            saveFile.quitTime = timeString;

            for (int index = 0; index < seaweeds.Count; index++)
            {
                SeaweedSave seaweedSave = seaweeds[index];
                if (SeaweedManager.Instance.seaweeds.ContainsKey(index))
                {
                    seaweedSave.seaweedLevel = SeaweedManager.Instance.seaweeds[index].currentLevel;
                }
            }

            saveFile.productionBonus = GameController.ResourcesManager.ProductionBonus;

            saveFile.seaweeds = seaweeds;
            saveFile.bubbles = GameController.ResourcesManager.BubbleCount.ToString();
            saveFile.specialBubbles = GameController.ResourcesManager.SpecialBubbleCount;
            saveFile.totalFishes = totalFishes;
        }

        private static string Now()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public void Read(in SaveFile saveFile)
        {
            DateTime savedTime;

            if (string.IsNullOrEmpty(saveFile.quitTime))
            {
                savedTime = DateTime.UtcNow; // Default to current time if quitTime is null or empty
            }
            else
            {
                try
                {
                    savedTime = DateTime.Parse(saveFile.quitTime);
                }
                catch (FormatException e)
                {
                    Debug.LogError("Failed to parse quitTime. Using current time as fallback.");
                    Debug.LogException(e);
                    savedTime = DateTime.Now;
                }
            }
            
            DateTime currentTime = DateTime.Now;
            TimeSpan timePassed = currentTime - savedTime;
            SecondsPassed = timePassed.TotalSeconds;
            
            GameController.ResourcesManager.AddBubbles(saveFile.bubbles);
            GameController.ResourcesManager.AddSpecialBubbles(saveFile.specialBubbles);
            
            seaweeds.Clear();
            seaweeds = saveFile.seaweeds;
            seaweeds.Sort((ctx1, ctx2) => ctx1.typeIndex.CompareTo(ctx2.typeIndex));
            
            totalFishes = saveFile.totalFishes;

            this.saveFile = saveFile;
        }

        public void LoadSeaweeds()
        {
            BigInteger bubbles = 0;
            foreach (SeaweedSave newSeaweed in seaweeds)
            {
                SeaweedData seaweedData = SeaweedManager.Instance.seaweedDatas[newSeaweed.typeIndex];
                
                float bubbleProduction = Mathf.RoundToInt(seaweedData.baseProduction) * Mathf.Pow(seaweedData.productionMultiplier, newSeaweed.seaweedLevel);
                float bubbleProductionRate = (bubbleProduction / seaweedData.productionCooldown) * saveFile.productionBonus;
                
                BigInteger bubblesProduced = Mathf.RoundToInt(bubbleProductionRate) * Mathf.RoundToInt((float)GameController.ProgressionManager.SecondsPassed);
                bubbles += bubblesProduced;
                
                GameController.ResourcesManager.AddBubbles(bubblesProduced.ToString());
            }
            Debug.Log($"Seaweeds produced {bubbles} bubbles while offline.");
        }

        public void AddSeaweed(Seaweed seaweed)
        {
            seaweeds.Add(new SeaweedSave
            {
                typeIndex = seaweed.data.seaweedType,
                seaweedLevel = seaweed.currentLevel,
            });
        }
    }
}