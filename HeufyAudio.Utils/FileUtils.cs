using System;
using System.IO;

namespace HeufyAudio.Utils
{
    public static class FileUtils
    {
        public static string AppDataFolder
        {
            get
            {
                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HeufyAudioRecorder");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
        }
    }
}
