using System;
using System.Drawing;

namespace AudioSharp.Utils
{
    public class RecordingItem
    {
        public string FileName { get; private set; }
        public string Extension { get; private set; }
        public DateTime CreationTime { get; private set; }
        public string Size { get; private set; }
        public Image Image { get; private set; }
        public string FullPath { get; private set; }

        public RecordingItem(string fileName, string extension, DateTime creationTime, string size, Image image, string fullPath)
        {
            FileName = fileName;
            Extension = extension;
            CreationTime = creationTime;
            Size = size;
            Image = image;
            FullPath = fullPath;
        }
    }
}
