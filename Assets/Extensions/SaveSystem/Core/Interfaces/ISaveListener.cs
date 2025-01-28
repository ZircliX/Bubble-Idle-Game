namespace SaveSystem.Core
{
    /// <summary>
    /// Objets that listens for save events of a specific type of file & read or write into it.
    /// </summary>
    public interface ISaveListener<T> : ISaveListener where T : ISaveFile
    {
        void Write(ref T saveFile);

        void Read(in T saveFile);
    }

    public interface ISaveListener
    {
        int Priority { get; }
    }
}