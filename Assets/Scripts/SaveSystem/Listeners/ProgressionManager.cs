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
        public float SecondsPassed { get; private set; }
        
        public void Write(ref SaveFile saveFile)
        {
            DateTime time = DateTime.Now;
            string timeString = time.ToString("o"); // ISO 8601 format
            saveFile.quitTime = timeString;
            
            saveFile.seaweeds = seaweeds;
            saveFile.bubbles = GameController.ResourcesManager.BubbleCount;
            
            Debug.Log("Write");
        }

        public void Read(in SaveFile saveFile)
        {
            seaweeds.Clear();

            DateTime savedTime = DateTime.Parse(saveFile.quitTime);
            DateTime currentTime = DateTime.Now;
            TimeSpan timePassed = currentTime - savedTime;
            SecondsPassed = (float)timePassed.TotalSeconds;
            
            GameController.ResourcesManager.AddBubbles(saveFile.bubbles);
            seaweeds = saveFile.seaweeds;
            
            Debug.Log("Read");
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