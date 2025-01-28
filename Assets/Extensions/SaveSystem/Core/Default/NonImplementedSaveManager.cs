using UnityEngine;

namespace SaveSystem.Core.Default
{
    /// <summary>
    /// Manager used when no manager was supplied by the user
    /// </summary>
    internal class NonImplementedSaveManager : ISaveManager
    {
        public bool Load<T, TS>(out T save, TS settings)
            where T : ISaveFile
            where TS : ISaveFileSettings<T>
        {
            Debug.LogWarning(
                $"Loading {nameof(NonImplementedSaveManager)} but no manager was implemented."
                );

            save = default;
            return false;
        }

        public bool Save<T, TS>(T save, TS settings)
            where T : ISaveFile
            where TS : ISaveFileSettings<T>
        {
            Debug.LogWarning(
                $"Saving {nameof(NonImplementedSaveManager)} but no manager was implemented."
                );
            return false;
        }
    }
}