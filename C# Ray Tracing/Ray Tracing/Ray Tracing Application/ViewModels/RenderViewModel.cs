
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RayTracingModel;
using RayTracingModel.Model;
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

        async public void UpdateImage()
        {
            Task<Color[,]> temp = Controller.GetInstance().Render();  // get the color array from the ray tracing project
            Color[,] colorArray = await temp;
            var width = colorArray.GetUpperBound(0) + 1;
            var height = colorArray.GetUpperBound(1) + 1;
            var stride = width * 4; // bytes per row

            byte[] pixelData = new byte[height * stride];

            for (int x = 0; x < width; ++x)
            {
                for (int z = 0; z < height; ++z)
                {
                    var color = colorArray[x, height-z-1];
                    var index = (z * stride) + (x * 4);

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
