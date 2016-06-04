using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace AudioSharp.GUI.Wpf
{
    public class GuiHelper
    {
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
