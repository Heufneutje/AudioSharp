using System.Collections.Generic;
using System.Windows.Forms;

namespace AudioSharp.Utils
{
    public class HotkeyUtils
    {
        public static readonly List<Keys> IllegalHotkeys = new List<Keys>
        {
            Keys.ControlKey,
            Keys.ShiftKey,
            Keys.Menu,
            Keys.Enter,
            Keys.NumLock,
            Keys.Capital,
            Keys.Scroll
        };

        public enum HotkeyType
        {
            StartRecording, StopRecording
        }
    }
}
