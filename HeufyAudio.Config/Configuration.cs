namespace HeufyAudio.Config
{
    public class Configuration
    {
        public string RecordingsFolder { get; set; }
        public string RecordingPrefix { get; set; }
        public int NextRecordingNumber { get; set; }
        public bool AutoIncrementRecordingNumber { get; set; }
        public string OutputFormat { get; set; }
        public bool ShowTrayIcon { get; set; }
        public bool MinimizeToTray { get; set; }
    }
}
