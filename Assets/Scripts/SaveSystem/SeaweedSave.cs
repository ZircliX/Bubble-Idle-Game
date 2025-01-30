using BubbleIdle.SeaweedSystem;
using UnityEngine;

namespace BubbleIdle.SaveSystem
{
    [System.Serializable]
    public struct SeaweedSave
    {
        public SeaweedData seaweedData;
        public int seaweedLevel;
        public Vector3 seaweedPosition;
    }
}