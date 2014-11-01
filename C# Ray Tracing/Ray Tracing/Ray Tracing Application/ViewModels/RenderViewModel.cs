
using System.Drawing;
using RayTracingModel;
using Color = System.Drawing.Color;

namespace Ray_Tracing_Application.ViewModels
{
    
    public class RenderViewModel
    {
        public static Bitmap renderImage { get; private set; }

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
            renderImage = image;
        }
    }
}
