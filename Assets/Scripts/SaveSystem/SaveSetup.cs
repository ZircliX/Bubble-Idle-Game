using SaveSystem.Core;
using UnityEngine;

namespace BubbleIdle.SaveSystem
{
    public class SaveSetup : MonoBehaviour
    {
        [SerializeField]
        private string prefName;

        private void Awake()
        {
            Save.SetSaveManager(new SaveManager());
        }

        private void OnEnable()
        {
            Save.Pull<SaveFile, SaveSettings>(out _, new SaveSettings()
            {
                prefName = prefName,
            });
        }

        private void OnDisable()
        {
            Save.Push<SaveFile, SaveSettings>(new SaveSettings()
            {
                prefName = prefName,
            });
        }
    }
}