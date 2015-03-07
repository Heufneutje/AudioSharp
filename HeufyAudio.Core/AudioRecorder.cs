using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace HeufyAudio.Core
{
    public class AudioRecorder : IDisposable
    {
        #region Fields & Properties
        private MMDevice _SelectedDevice;
        private WasapiCapture _CaptureStream;
        private WaveFileWriter _OutWriter;
        public MMDeviceCollection Devices { get; private set; }
        public MMDevice SelectedDevice
        {
            get
            {
                return _SelectedDevice;
            }
            set
            {
                _SelectedDevice = value;
                if (_CaptureStream != null)
                {
                    _CaptureStream.StopRecording();
                    _CaptureStream.Dispose();
                }
                _CaptureStream = new WasapiCapture(value);
                // _CaptureStream.WaveFormat = new WaveFormat(44100, _SelectedDevice.AudioEndpointVolume.Channels.Count);
                _CaptureStream.StartRecording();
            }
        }
        public MMDevice DefaultDevice
        {
            get
            {
                MMDevice defaultDev = new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
                return Devices.Where(x => x.ID == defaultDev.ID).FirstOrDefault();
            }
        }
        public int DefaultDeviceNumber
        {
            get
            {
                MMDevice defaultDev = new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
                for (int i = 0; i < Devices.Count; i++)
                {
                    if (Devices[i].ID == defaultDev.ID)
                        return i;
                }
                return -1;
            }
        }
        #endregion

        #region Constructors
        public AudioRecorder()
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            Devices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
        }
        #endregion

        #region Events
        private void CaptureStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (_OutWriter == null)
                return;

            _OutWriter.Write(e.Buffer, 0, e.BytesRecorded);
            _OutWriter.Flush();
        }
        #endregion

        #region Public Functions
        public void StartRecording(string fileName)
        {
            _OutWriter = new WaveFileWriter(fileName, _CaptureStream.WaveFormat);
            _CaptureStream.DataAvailable += CaptureStream_DataAvailable;
        }

        public void StopRecording()
        {
            _CaptureStream.DataAvailable -= CaptureStream_DataAvailable;
            _OutWriter.Dispose();
            _OutWriter = null;
        }

        public void Dispose()
        {
            if (_CaptureStream != null)
            {
                _CaptureStream.StopRecording();
                _CaptureStream.Dispose();
                _CaptureStream = null;
            }
            if (_OutWriter != null)
            {
                _OutWriter.Dispose();
                _OutWriter = null;
            }
        }
        #endregion
    }
}
