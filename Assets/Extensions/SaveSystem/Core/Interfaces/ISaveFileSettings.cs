namespace SaveSystem.Core
{
    /// <summary>
    /// Base interface that can provide specific settings on the way a <see cref="ISaveFile"/> should be loaded.
    /// </summary>
    /// <typeparam name="T">Supported file</typeparam>
    public interface ISaveFileSettings<out T> where T : ISaveFile
    {
        /// <summary>
        /// If no <see cref="ISaveFile"/>  was found, this method provides the default save file (usually, the start save file)
        /// </summary>
        /// <returns> New <see cref="ISaveFile"/> </returns>
        T GetDefaultSaveFile();
    }
}