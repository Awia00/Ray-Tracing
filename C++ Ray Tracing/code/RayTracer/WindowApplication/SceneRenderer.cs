using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RayTracer;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Point = System.Drawing.Point;

namespace WindowApplication
{
    public class SceneRenderer
    {
        private readonly int _width;
        private readonly int _height;

        public SceneRenderer(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public async Task<ImageSource> Render()
        {
            var output = new Bitmap(_width, _height);

            var scene = new Scene(output.Width, output.Height, 30);
            var result = await Task.Run(()=>scene.render());

            for (int i = 0; i < result.Length; i++)
            {
                var x = (int)(i % output.Width);
                var y = (int)(i / output.Width);

                var rcolor = result[i];
                var color = Color.FromArgb(rcolor.red, rcolor.green, rcolor.blue);
                
                output.SetPixel(x,y,color);
            }

            return ConvertBitmap(output);
        }

        // SOURCE http://stackoverflow.com/questions/2284353/is-there-a-good-way-to-convert-between-bitmapsource-and-bitmap
        public static BitmapSource ConvertBitmap(Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                          source.GetHbitmap(),
                          IntPtr.Zero,
                          Int32Rect.Empty,
                          BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
