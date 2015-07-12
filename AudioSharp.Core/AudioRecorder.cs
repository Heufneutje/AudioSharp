using System;
using System.Linq;
using NAudio.CoreAudioApi;

namespace AudioSharp.Core
{
    public abstract class AudioRecorder : IDisposable
    {
        #region Fields & Properties
        private MMDevice _SelectedDevice;
        protected WasapiCapture _CaptureStream;
        
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

        #region Virtual Functions
        public virtual void StartRecording(string fileName)
        {
            
        }

        public virtual void StopRecording()
        {
            
        }

        public virtual void Dispose()
        {
            if (_CaptureStream != null)
            {
                _CaptureStream.StopRecording();
                _CaptureStream.Dispose();
                _CaptureStream = null;
            }
        }
        #endregion
    }
}
