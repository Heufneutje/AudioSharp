using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeufyAudio.Config
{
    public class Configuration
    {
        public string RecordingsFolder { get; set; }
        public string RecordingPrefix { get; set; }
        public int CurrentRecordingNumber { get; set; }
        public string NextRecordingPath
        {
            get
            {
                return String.Format("{0}.wav", Path.Combine(RecordingsFolder, RecordingPrefix + CurrentRecordingNumber.ToString("D3")));
            }
        }
    }
}
