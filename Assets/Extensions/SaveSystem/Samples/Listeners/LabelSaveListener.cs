using System;
using SaveSystem.Core;
using UnityEngine;

namespace SaveSystem.Samples
{
    public class LabelSaveListener : MonoBehaviour, ISaveListener<SampleSaveFile>
    {
        public int Priority => 2;

        [SerializeField]
        private string label;

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
            saveFile.label = label;
        }

        public void Read(in SampleSaveFile saveFile)
        {
            label = saveFile.label;
        }
    }
}