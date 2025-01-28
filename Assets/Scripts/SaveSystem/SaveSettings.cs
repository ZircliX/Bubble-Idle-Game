using SaveSystem.Core;

namespace BubbleIdle.SaveSystem
{
    public struct SaveSettings : ISaveFileSettings<SaveFile>
    {
        public string prefName;
        public SaveFile GetDefaultSaveFile()
        {
            return new SaveFile()
            {
                number = 3,
                label = "Default"
            };
        }
    }
}