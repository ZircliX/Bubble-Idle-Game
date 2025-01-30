using System.Collections.Generic;
using SaveSystem.Core;
using UnityEngine;

namespace BubbleIdle.SaveSystem
{
    [System.Serializable]
    public struct SaveFile : ISaveFile
    {
        public int Version => 1;

        [SerializeField] public List<SeaweedSave> seaweeds;
        [SerializeField] public string quitTime;
        [SerializeField] public float secondsPassed;
        
        [SerializeField] public long bubbles;
    }
}