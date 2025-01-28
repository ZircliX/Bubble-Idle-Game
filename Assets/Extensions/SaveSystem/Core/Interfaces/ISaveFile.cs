namespace SaveSystem.Core
{
    /// <summary>
    /// Core of the system. This is the file that will be saved with this system.
    /// </summary>
    public interface ISaveFile
    {
        /// <summary>
        /// Used for migrations (TODO)
        /// </summary>
        int Version { get; }
    }
}