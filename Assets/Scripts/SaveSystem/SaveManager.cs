using SaveSystem.Core;
using UnityEngine;

namespace BubbleIdle.SaveSystem
{
    public class SaveManager : ISaveManager
    {
        public bool Load<T, TS>(out T save, TS settings) where T : ISaveFile where TS : ISaveFileSettings<T>
        {
            if (settings is SaveSettings sampleSaveSettings)
            {
                if (typeof(T) == typeof(SaveFile) && PlayerPrefs.HasKey(sampleSaveSettings.prefName))
                {
                    string json = PlayerPrefs.GetString(sampleSaveSettings.prefName);
                    save = JsonUtility.FromJson<T>(json);
                    return save != null;
                }
            }

            save = default;
            return false;
        }

        public bool Save<T, TS>(T save, TS settings) where T : ISaveFile where TS : ISaveFileSettings<T>
        {
            if (settings is SaveSettings sampleSaveSettings)
            {
                string json = JsonUtility.ToJson(save, true);
                PlayerPrefs.SetString(sampleSaveSettings.prefName, json);
            }

            return true;
        }
    }
}