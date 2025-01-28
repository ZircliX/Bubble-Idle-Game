using SaveSystem.Core;
using UnityEngine;

namespace SaveSystem.Samples
{
    public class SampleSaveManager : ISaveManager
    {
        public bool Load<T, TS>(out T save, TS settings)
            where T : ISaveFile
            where TS : ISaveFileSettings<T>
        {

            if (settings is SampleSaveSettings sampleSaveSettings)
            {
                if (typeof(T) == typeof(SampleSaveFile) && PlayerPrefs.HasKey(sampleSaveSettings.prefName))
                {
                    string json = PlayerPrefs.GetString(sampleSaveSettings.prefName);
                    save = JsonUtility.FromJson<T>(json);
                    return save != null;
                }
            }

            save = default;
            return false;
        }

        public bool Save<T, TS>(T save, TS settings)
            where T : ISaveFile
            where TS : ISaveFileSettings<T>
        {
            if (settings is SampleSaveSettings sampleSaveSettings)
            {
                string json = JsonUtility.ToJson(save);
                PlayerPrefs.SetString(sampleSaveSettings.prefName, json);
            }

            return true;
        }
    }
}