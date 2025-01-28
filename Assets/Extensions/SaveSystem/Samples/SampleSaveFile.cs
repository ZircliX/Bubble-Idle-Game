using SaveSystem.Core;
using UnityEngine;

namespace SaveSystem.Samples
{
    [System.Serializable]
    public struct SampleSaveFile : ISaveFile
    {
        public int Version => 1;

        [SerializeField]
        public int number;

        [SerializeField]
        public string label;
    }
}