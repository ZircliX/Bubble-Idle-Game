namespace SaveSystem.Core
{
    /// <summary>
    /// Object responsible for the way a file is saved.
    /// Is it on steam? On android? Using PlayerPrefs?
    /// You choose
    /// </summary>
    public interface ISaveManager
    {
        bool Load<T, TS>(out T save, TS settings)
            where T : ISaveFile
            where TS : ISaveFileSettings<T>;

        bool Save<T, TS>(T save, TS settings)
            where T : ISaveFile
            where TS : ISaveFileSettings<T>;
    }
}