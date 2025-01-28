using System.Collections.Generic;
using SaveSystem.Core;
using UnityEngine;

namespace BubbleIdle.SaveSystem
{
    public class ProgressionManager : ISaveListener<SaveFile>
    {
        public int Priority => 2;
        
        public Dictionary<string, bool> seaweedStatus { get; private set; } = new Dictionary<string, bool>();

        private int num;

        public void Write(ref SaveFile saveFile)
        {
            saveFile.number = num;
            Debug.Log("Write");
        }

        public void Read(in SaveFile saveFile)
        {
            num = saveFile.number;
            Debug.Log("Read");
        }
    }
}