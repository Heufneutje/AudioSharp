using System;
using System.Linq;
using NAudio.CoreAudioApi;

namespace AudioSharp.Core
{
    public abstract class AudioRecorder : IDisposable
    {
        #region Fields & Properties
        private MMDevice _selectedDevice;
        protected WasapiCapture _captureStream;
        
        public MMDeviceCollection Devices { get; private set; }
        public MMDevice SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                _selectedDevice = value;
                if (_captureStream != null)
                {
                    _captureStream.StopRecording();
                    _captureStream.Dispose();
                }
                _captureStream = new WasapiCapture(value);
                // _CaptureStream.WaveFormat = new WaveFormat(44100, _SelectedDevice.AudioEndpointVolume.Channels.Count);
                _captureStream.StartRecording();
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

        #region Virtual Functions
        public abstract void StartRecording(string fileName);

        public abstract void StopRecording();

        public virtual void Dispose()
        {
            if (_captureStream != null)
            {
                _captureStream.StopRecording();
                _captureStream.Dispose();
                _captureStream = null;
            }
        }
        #endregion
    }
}
