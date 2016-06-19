using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AudioSharp.GUI.Wpf
{
    /// <summary>
    /// Interaction logic for WaveFormPlotter.xaml
    /// </summary>
    public partial class WaveFormPlotter : UserControl
    {
        private Polyline _polyline;
        private double _canvasHeight = 0;
        private double _canvasWidth = 0;
        private double _polylineHeight = 0;
        private double _polylineWidth = 0;

        private Queue<Point> _displayPoints;
        private Queue<int> _displayNumbers;

        private long _count = 0;
        private int _numToDisplay = 2205;

        private delegate void PlotDataInvoke(byte[] data, int bytesRecorded);

        public WaveFormPlotter()
        {
            InitializeComponent();

            _canvasHeight = waveFormCanvas.Height;
            _canvasWidth = waveFormCanvas.Width;

            _polyline = new Polyline()
            {
                Name = "waveform",
                Stroke = Brushes.Blue,
                StrokeThickness = 1,
                MaxHeight = _canvasHeight - 4,
                MaxWidth = _canvasWidth - 4
            };

            _polylineHeight = _polyline.MaxHeight;
            _polylineWidth = _polyline.MaxWidth;
            _displayPoints = new Queue<Point>();
            _displayNumbers = new Queue<int>();
        }

        public void PlotData(byte[] data, int bytesRecorded)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new PlotDataInvoke(PlotData), data, bytesRecorded);
                return;
            }

            byte[] number = new byte[4];

            for (int i = 0; i < bytesRecorded - 1; i += 100)
            {
                number[0] = data[i];
                number[1] = data[i + 1];
                number[2] = data[i + 2];
                number[3] = data[i + 3];
                if (_count < _numToDisplay)
                {
                    _displayNumbers.Enqueue(BitConverter.ToInt32(number, 0));
                    ++_count;
                }
                else
                {
                    _displayNumbers.Dequeue();
                    _displayNumbers.Enqueue(BitConverter.ToInt32(number, 0));
                }
            }
            waveFormCanvas.Children.Clear();
            _polyline.Points.Clear();
            int[] shts2 = _displayNumbers.ToArray();
            for (int x = 0; x < shts2.Length; ++x)
            {
                _polyline.Points.Add(Normalize(x, shts2[x]));
            }
            
            waveFormCanvas.Children.Add(_polyline);
        }

        private Point Normalize(int x, int y)
        {
            Point point = new Point();
            point.X = 1.0 * x / _numToDisplay * _polylineWidth;
            point.Y = _polylineHeight / 2.0 - y / (int.MaxValue * 1.0) * (_polylineHeight / 2.0);
            return point;
        }
    }
}
