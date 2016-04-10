using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace AudioSharp.Utils
{
    public class HotkeyUtils
    {
        private static int _HotkeyCount;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public enum HotkeyType
        {
            StartRecording, StopRecording
        }

        public static readonly List<Key> IllegalHotkeys = new List<Key>
        {
            Key.Enter,
            Key.NumLock,
            Key.Capital,
            Key.Scroll
        };

        public static void RegisterAllHotkeys(IntPtr handle, Dictionary<HotkeyType, Tuple<Key, Key, int>> globalHotkeys)
        {
            _HotkeyCount = 1;
            foreach (Tuple<Key, Key, int> globalHotkey in globalHotkeys.Values)
            {
                // TODO: Handle hotkey register failures.
                RegisterHotKey(handle, _HotkeyCount, globalHotkey.Item3, (int)globalHotkey.Item1);
                _HotkeyCount++;
            }
        }

        public static void UnregisterAllHotkeys(IntPtr handle)
        {
            for (int i = 1; i <= _HotkeyCount; i++)
            {
                UnregisterHotKey(handle, i);
            }
        }
    }
}
