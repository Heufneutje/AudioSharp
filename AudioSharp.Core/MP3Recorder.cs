using NAudio.Lame;
using NAudio.Wave;

namespace AudioSharp.Core
{
    public class MP3Recorder : AudioRecorder
    {
        #region Properties & Fields

        private LameMP3FileWriter _outWriter;
        private int _bitRate;

        #endregion Properties & Fields

        #region Constructors
        public MP3Recorder(int bitRate)
        {
            _bitRate = bitRate;
        }
        #endregion

        #region Overrides

        public override void StartRecording(string fileName)
        {
            _outWriter = new LameMP3FileWriter(fileName, _captureStream.WaveFormat, (LAMEPreset)_bitRate);
            _captureStream.DataAvailable += CaptureStream_DataAvailable;
        }

        public override void StopRecording()
        {
            base.StopRecording();
            _captureStream.DataAvailable -= CaptureStream_DataAvailable;
            _outWriter.Dispose();
            _outWriter = null;
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_outWriter != null)
            {
                _outWriter.Dispose();
                _outWriter = null;
            }
        }

        #endregion Overrides

        #region Events

        private void CaptureStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (_outWriter == null)
                return;

            _outWriter.Write(e.Buffer, 0, e.BytesRecorded);
        }

        #endregion Events
    }
}
