using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AudioSharp.GUI.CustomControls
{
    /// <summary>
    /// Custom control to display a button in a TextBox.
    /// </summary>
    public class ButtonTextBox : TextBox
    {
        #region Fields & Custom Properies
        private Button _textBoxButton;

        public static DependencyProperty ButtonImageProperty = DependencyProperty.Register("ButtonImage", typeof(ImageSource), typeof(ButtonTextBox));

        public ImageSource ButtonImage
        {
            get
            {
                return (ImageSource)GetValue(ButtonImageProperty);
            }
            set
            {
                SetValue(ButtonImageProperty, value);
            }
        }

        public static DependencyProperty ButtonToolTipProperty = DependencyProperty.Register("ButtonToolTip", typeof(string), typeof(ButtonTextBox));

        public string ButtonToolTip
        {
            get
            {
                return (string)GetValue(ButtonToolTipProperty);
            }
            set
            {
                SetValue(ButtonToolTipProperty, value);
            }
        }
        #endregion

        #region Custom Events
        public static readonly RoutedEvent ButtonClickEvent = EventManager.RegisterRoutedEvent("ButtonClick", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ButtonTextBox));

        public event RoutedEventHandler ButtonClick
        {
            add { AddHandler(ButtonClickEvent, value); }
            remove { RemoveHandler(ButtonClickEvent, value); }
        }

        private void TextBoxButtonClicked(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ButtonClickEvent));
        }
        #endregion

        #region Constructor
        static ButtonTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonTextBox), new FrameworkPropertyMetadata(typeof(ButtonTextBox)));
        }
        #endregion

        #region Overrides
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _textBoxButton = GetTemplateChild("PART_TextBoxButton") as Button;

            if (_textBoxButton != null)
                _textBoxButton.Click += TextBoxButtonClicked;
        }
        #endregion
    }
}
