using System;
using System.Collections.Generic;
using SaveSystem.Core;

namespace BubbleIdle.SaveSystem
{
    public struct SaveSettings : ISaveFileSettings<SaveFile>
    {
        public string prefName;
        public SaveFile GetDefaultSaveFile()
        {
            return new SaveFile()
            {
                seaweeds = new List<SeaweedSave>(),
                totalFishes = 0,
                quitTime = DateTime.Now.ToString("o"),
                secondsPassed = 0f,
                productionBonus = 1,
                bubbles = "5000000"
            };
        }
    }
}