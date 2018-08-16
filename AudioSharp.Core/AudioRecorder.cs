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

        public bool IsPaused { get; private set; }
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
                MMDevice defaultDev = null;
                if (Devices.Any())
                    defaultDev = new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);

                for (int i = 0; i < Devices.Count; i++)
                {
                    if (Devices[i].ID == defaultDev.ID)
                        return i;
                }
                return -1;
            }
        }

        #endregion Fields & Properties

        #region Constructors

        public AudioRecorder()
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            Devices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
        }

        #endregion Constructors

        #region Virtual Functions

        public abstract void StartRecording(string fileName);

        public virtual void PauseRecording()
        {
            if (IsPaused)
                _captureStream.StartRecording();
            else
                _captureStream.StopRecording();

            IsPaused = !IsPaused;
        }

        public virtual void StopRecording()
        {
            IsPaused = false;
        }

        public virtual void Dispose()
        {
            if (_captureStream != null)
            {
                _captureStream.StopRecording();
                _captureStream.Dispose();
                _captureStream = null;
            }
        }

        #endregion Virtual Functions
    }
}
