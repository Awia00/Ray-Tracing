
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using RayTracingModel;
using Ray_Tracing_Application.Annotations;
using Color = System.Drawing.Color;

namespace Ray_Tracing_Application.ViewModels
{

    public class RenderViewModel : INotifyPropertyChanged
    {
        private static Bitmap _renderImage;

        public static Bitmap RenderImage
        {
            get { return _renderImage; }
            private set
            {
                _renderImage = value;
                //OnPropertyChanged("RenderImage");
                // todo move the update method to the xaml.cs window and make it cfhange the source such that it updates (without true databinding since its being changed everytime)
            }
        }

        public RenderViewModel()
        {
            UpdateImage();
        }

        public void UpdateImage()
        {
            Color[,] colorArray = Scene.getInstance().render();  // get the color array from the ray tracing project

            Bitmap image = new Bitmap(colorArray.GetLength(0),colorArray.GetLength(1));
            for (int i = 0; i < colorArray.GetLength(0); i++)
            {
                for (int j = 0; j < colorArray.GetLength(1); j++)
                {
                    image.SetPixel(i,j,colorArray[i,j]);
                }
            }
            RenderImage = image;
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
