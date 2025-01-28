using System;
using SaveSystem.Core;
using UnityEngine;

namespace SaveSystem.Samples
{
    public class NumberSaveListener : MonoBehaviour, ISaveListener<SampleSaveFile>
    {
        public int Priority => 1;

        [SerializeField]
        private int number;

        private void OnEnable()
        {
            Save.AddListener(this);
        }

        private void OnDisable()
        {
            Save.RemoveListener(this);
        }

        public void Write(ref SampleSaveFile saveFile)
        {
            saveFile.number = number;
            Debug.Log("Write");
        }

        public void Read(in SampleSaveFile saveFile)
        {
            number = saveFile.number;
            Debug.Log("Read");
        }
    }
}