using System;
using System.Collections.Generic;
using SaveSystem.Core.Default;
using UnityEngine;

namespace SaveSystem.Core
{
    /// <summary>
    /// Entry point of the system.
    /// </summary>
    public static class Save
    {
        public static Action<ISaveManager> OnSaveManagerChanged;
        public static Action<ISaveFile> OnFileSaved;
        public static Action<ISaveFile> OnFileLoaded;

        /// <summary>
        /// Current manager used for saving the game's state
        /// </summary>
        public static ISaveManager Manager { get; private set; }

        private static readonly List<ISaveListener> SaveListeners;

        /// <summary>
        /// Invoked at the start of the game
        /// </summary>
        static Save()
        {
            SaveListeners = new List<ISaveListener>();
            Manager = new NonImplementedSaveManager();
        }

        /// <summary>
        /// Set the new way of saving the game.
        /// </summary>
        /// <param name="manager">New manager</param>
        public static void SetSaveManager(ISaveManager manager)
        {
            Manager = manager;
            OnSaveManagerChanged?.Invoke(manager);
        }

        /// <summary>
        /// Adds an objet that will listen for a specific type of file events
        /// </summary>
        /// <param name="listener"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>True if added with success</returns>
        public static bool AddListener<T>(ISaveListener<T> listener) where T : ISaveFile
        {
            if(SaveListeners.Contains(listener))
                return false;

            SaveListeners.Add(listener);
            SaveListeners.Sort((a, b) =>
            {
                return a.Priority.CompareTo(b.Priority);
            });

            return true;
        }

        /// <summary>
        /// Removes an objet that will no longer listen for a specific type of file events
        /// </summary>
        /// <param name="listener"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>True if removed with success</returns>
        public static bool RemoveListener<T>(ISaveListener<T> listener) where T : ISaveFile
        {
            return SaveListeners.Remove(listener);
        }

        /// <summary>
        /// Saves a new file.
        /// </summary>
        /// <param name="settings"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TS"></typeparam>
        /// <returns>True if saved with success</returns>
        public static bool Push<T, TS>(TS settings = default)
            where T : ISaveFile, new()
            where TS : ISaveFileSettings<T>
        {
            T save = new T();
            return Push(save, settings);
        }

        /// <summary>
        /// Saves a new file.
        /// </summary>
        /// <param name="settings"></param>
        /// <typeparam name="T">Custom base file before writting</typeparam>
        /// <typeparam name="TS"></typeparam>
        /// <returns>True if saved with success</returns>
        public static bool Push<T, TS>(T saveFile, TS settings = default)
            where T : ISaveFile
            where TS : ISaveFileSettings<T>
        {
            try
            {
                foreach (ISaveListener saveListener in SaveListeners)
                {
                    //Every listener concerned writes into the file
                    if (saveListener is ISaveListener<T> typedListener)
                        typedListener.Write(ref saveFile);
                }


                bool result = Manager.Save(saveFile, settings);
                if (result)
                    OnFileSaved?.Invoke(saveFile);

                return result;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }

        /// <summary>
        /// Loads a new file
        /// </summary>
        /// <param name="file">Output if success</param>
        /// <param name="settings"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TS"></typeparam>
        /// <returns>True if loaded with success</returns>
        public static bool Pull<T, TS>(out T file, TS settings = default)
            where T : ISaveFile
            where TS : ISaveFileSettings<T>
        {
            try
            {
                if (!Manager.Load(out file, settings))
                    file = settings.GetDefaultSaveFile();

                foreach (ISaveListener saveListener in SaveListeners)
                {
                    //Every listener concerned reads the file
                    if (saveListener is ISaveListener<T> typedListener)
                        typedListener.Read(in file);
                }

                OnFileLoaded?.Invoke(file);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                file = default;
                return false;
            }
        }
    }
}