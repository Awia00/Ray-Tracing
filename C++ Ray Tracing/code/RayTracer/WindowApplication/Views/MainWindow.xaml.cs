using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WindowApplication.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool inProgress = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private static byte[] ConvertData(Color[] data)
        {
            var rnd = new Random();
            var output = new List<byte>();
            foreach (var color in data)
            {
                output.Add((byte)rnd.Next(255));
                output.Add((byte)rnd.Next(255));
                output.Add((byte)rnd.Next(255));
            }
            return output.ToArray();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (!inProgress)
            {
                inProgress = true;
                StatusLabel.Content = "In progress...";
                var renderer = new SceneRenderer((int)TheGrid.ActualWidth, (int)TheGrid.ActualHeight);
                RenderBitmap.Source = await renderer.Render();
                StatusLabel.Content = "Done";
                inProgress = false;
            }
            
        }
    }
}
