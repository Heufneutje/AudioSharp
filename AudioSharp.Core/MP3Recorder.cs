using NAudio.Lame;
using NAudio.Wave;

namespace AudioSharp.Core
{
    public class MP3Recorder : AudioRecorder
    {
        #region Properties & Fields
        private LameMP3FileWriter _OutWriter;
        #endregion

        #region Overrides
        public override void StartRecording(string fileName)
        {
            _OutWriter = new LameMP3FileWriter(fileName, _CaptureStream.WaveFormat, 128);
            _CaptureStream.DataAvailable += CaptureStream_DataAvailable;
        }

        public override void StopRecording()
        {
            _CaptureStream.DataAvailable -= CaptureStream_DataAvailable;
            _OutWriter.Dispose();
            _OutWriter = null;
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_OutWriter != null)
            {
                _OutWriter.Dispose();
                _OutWriter = null;
            }
        }
        #endregion

        #region Events
        private void CaptureStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (_OutWriter == null)
                return;

            _OutWriter.Write(e.Buffer, 0, e.BytesRecorded);
        }
        #endregion
    }
}
