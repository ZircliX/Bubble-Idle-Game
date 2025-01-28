using SaveSystem.Core;
using UnityEngine;

namespace BubbleIdle.SaveSystem
{
    [System.Serializable]
    public struct SaveFile : ISaveFile
    {
        public int Version => 1;

        [SerializeField] public int number;
        [SerializeField] public string label;
    }
}