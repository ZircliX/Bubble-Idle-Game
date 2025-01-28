using System;
using SaveSystem.Core;
using UnityEngine;

namespace SaveSystem.Samples
{
    public class SampleSetup : MonoBehaviour
    {
        [SerializeField]
        private string prefName;

        private void Awake()
        {
            Save.SetSaveManager(new SampleSaveManager());
        }

        private void OnEnable()
        {
            Save.Pull<SampleSaveFile, SampleSaveSettings>(out _, new SampleSaveSettings()
            {
                prefName = prefName,
            });
        }

        private void OnDisable()
        {
            Save.Push<SampleSaveFile, SampleSaveSettings>(new SampleSaveSettings()
            {
                prefName = prefName,
            });
        }
    }
}