using SaveSystem.Core;

namespace SaveSystem.Samples
{
    public struct SampleSaveSettings : ISaveFileSettings<SampleSaveFile>
    {
        public string prefName;

        public SampleSaveFile GetDefaultSaveFile()
        {
            return new SampleSaveFile()
            {
                number = 3,
                label = "Default"
            };
        }
    }
}