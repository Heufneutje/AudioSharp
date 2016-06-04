using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioSharp.Utils
{
    public class RecordingItem
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public DateTime CreationTime { get; set; }
        public string Size { get; set; }
        public Image Image { get; set; }
        public string FullPath { get; set; }
    }
}
