using System.Collections.Generic;
using BubbleIdle.SeaweedSystem;
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
    }

    [System.Serializable]
    public struct SeaweedSave
    {
        public SeaweedData seaweedData;
        public int seaweedLevel;
        public Vector3 seaweedPosition;
    }
}