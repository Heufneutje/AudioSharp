using System;
using System.IO;
using System.Linq;

namespace AudioSharp.Utils
{
    public class DriveSpaceUtils
    {
        public static string GetAvailableDriveSpace(string recordingPath)
        {
            DriveInfo info = DriveInfo.GetDrives().Where(x => x.Name == Path.GetPathRoot(recordingPath)).FirstOrDefault();
            return BytesTostring(info.TotalFreeSpace);
        }
        
        public static string BytesTostring(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0 B";
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return string.Format("{0} {1}", Math.Sign(byteCount) * num, suf[place]);
        }
    }
}
