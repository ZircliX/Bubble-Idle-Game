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
                quitTime = DateTime.Now.ToString("o"),
                secondsPassed = 0f
            };
        }
    }
}