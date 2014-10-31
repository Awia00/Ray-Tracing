using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ray_Tracing_Application.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void drawCanvas()
        {
            //int width = 200;
            //int height = 200;

            ////
            //// Here is the pixel format of your data, set it to the proper value for your data
            ////
            //PixelFormat pf = PixelFormats.Bgr32;
            //int rawStride = (width * pf.BitsPerPixel + 7) / 8;

            ////
            //// Here is your raw data
            ////
            //int[] rawImage = new int[rawStride * height / 4];


            ////
            //// Create the BitmapSource
            ////
            //BitmapSource bitmap = BitmapSource.Create(
            //    width, height,
            //    96, 96, pf, null,
            //    rawImage, rawStride);
        }
    }
}
