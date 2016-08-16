using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace AudioSharp.GUI.Wpf
{
    public static class GuiHelper
    {
        private const int _GWL_STYLE = -16;
        private const int _WS_MAXIMIZEBOX = 0x10000;
        private const int _WS_MINIMIZEBOX = 0x20000;

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int value);

        internal static void HideMinimizeAndMaximizeButtons(this Window window)
        {
            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(window).Handle;
            int currentStyle = GetWindowLong(hwnd, _GWL_STYLE);

            SetWindowLong(hwnd, _GWL_STYLE, (currentStyle & ~_WS_MAXIMIZEBOX & ~_WS_MINIMIZEBOX));
        }

        public static void UpdateBindingTarget(Control control, DependencyProperty dp)
        {
            BindingExpression expression = control.GetBindingExpression(dp);
            expression.UpdateTarget();
        }

        public static void UpdateTextBoxBindingTarget(TextBox textBox, DependencyProperty dp = null)
        {
            if (dp == null)
                dp = TextBox.TextProperty;

            UpdateBindingTarget(textBox, dp);
        }

        public static void UpdateToggleButtonBindingTarget(ToggleButton toggleButton, DependencyProperty dp = null)
        {
            if (dp == null)
                dp = ToggleButton.IsCheckedProperty;

            UpdateBindingTarget(toggleButton, dp);
        }
    }
}
