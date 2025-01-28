using SaveSystem.Core;
using UnityEngine;

namespace BubbleIdle.SaveSystem
{
    public class NumberSaveListener : MonoBehaviour, ISaveListener<SaveFile>
    {
        public int Priority => 2;
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

        public void Write(ref SaveFile saveFile)
        {
            saveFile.number = number;
            Debug.Log("Write");
        }

        public void Read(in SaveFile saveFile)
        {
            number = saveFile.number;
            Debug.Log("Read");
        }
    }
}