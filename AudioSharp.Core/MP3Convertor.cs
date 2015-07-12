using System;
using System.IO;
using NAudio.Lame;
using NAudio.Wave;

namespace AudioSharp.Core
{
    public class MP3Convertor
    {
        public event EventHandler<ConvertorProgressEventArgs> ReportingProgress;

        public void WaveToMP3(string filePath)
        {
            byte[] wavBytes = File.ReadAllBytes(filePath);
            FileInfo wavInfo = new FileInfo(filePath);

            using (MemoryStream retMs = new MemoryStream())
            using (MemoryStream ms = new MemoryStream(wavBytes))
            using (WaveFileReader rdr = new WaveFileReader(ms))
            using (LameMP3FileWriter wtr = new LameMP3FileWriter(retMs, rdr.WaveFormat, 128))
            {
                byte[] buffer = new byte[16 * 1024];
                long size = rdr.Length;
                long runningTotal = 0;

                int read;
                while ((read = rdr.Read(buffer, 0, buffer.Length)) > 0)
                {
                    wtr.Write(buffer, 0, read);
                    runningTotal += read;
                    if (ReportingProgress != null)
                    {
                        double progress = runningTotal / (double)size * 100;
                        ConvertorProgressEventArgs args = new ConvertorProgressEventArgs() { Progress = Convert.ToInt32(progress) };
                        ReportingProgress(this, args);
                    }
                }

                string mp3Path = string.Format("{0}.mp3", wavInfo.FullName.Substring(0, wavInfo.FullName.LastIndexOf('.')));
                File.WriteAllBytes(mp3Path, retMs.ToArray());
            }
        }
    }

    public class ConvertorProgressEventArgs : EventArgs
    {
        public int Progress { get; set; }
    }
}
