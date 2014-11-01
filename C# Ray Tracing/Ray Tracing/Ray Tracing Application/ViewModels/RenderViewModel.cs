
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RayTracingModel;
using Ray_Tracing_Application.Annotations;
using Color = System.Drawing.Color;

namespace Ray_Tracing_Application.ViewModels
{

    public class RenderViewModel : INotifyPropertyChanged
    {
        private BitmapSource _renderBitMapSource;
        public BitmapSource RenderBitmap
        {
            get { return _renderBitMapSource; }
            set
            {
                _renderBitMapSource = value;
            OnPropertyChanged("RenderBitmap");
            }
        }

        public RenderViewModel()
        {
            UpdateImage();
        }

        public void UpdateImage()
        {
            Color[,] colorArray = Scene.getInstance().render();  // get the color array from the ray tracing project

            var height = colorArray.GetUpperBound(0) + 1;
            var width = colorArray.GetUpperBound(1) + 1;
            var stride = width * 4; // bytes per row

            byte[] pixelData = new byte[height * stride];

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    var color = colorArray[y, x];
                    var index = (y * stride) + (x * 4);

                    pixelData[index] = color.B;
                    pixelData[index + 1] = color.G;
                    pixelData[index + 2] = color.R;
                    pixelData[index + 3] = color.A;
                }
            }

            RenderBitmap = BitmapSource.Create(width, height, 96, 96, PixelFormats.Bgra32, null, pixelData, stride);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
