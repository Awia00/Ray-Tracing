using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracingModel.Model.Objects3D;

namespace RayTracingModel.Model.Cameras
{
    public class SimpleCamera : ICamera
    {
        public Vector3D Direction { get; set; }
        public Vector3D Eye { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int AmtOfHeightPixels { get; set; }
        public int AmtOfWidthPixels { get; set; }

        private Line3D[,] _cameraRays = null;
        public Line3D[,] GenerateCameraVectors()
        {
            if (_cameraRays == null)
            {
                var tempCameraVectors = new Line3D[AmtOfWidthPixels, AmtOfHeightPixels];
                double pixelWidth = Width/(double)AmtOfWidthPixels;
                double pixelHeight = Height /(double)AmtOfHeightPixels;
                for (int i = 0; i < AmtOfWidthPixels; i++)
                {
                    for (int j = 0; j < AmtOfHeightPixels; j++)
                    {
                        var tempDirectionVector = new Vector3D(
                            (-Width / 2 + i * pixelWidth),
                            (Direction.Length),
                            (-Height / 2 + j * pixelHeight));
                        tempCameraVectors[i, j] = new Line3D(Eye, tempDirectionVector.Normalize());
                    }
                }
                _cameraRays = tempCameraVectors;
            }
            return _cameraRays;

        }

        public SimpleCamera(Vector3D direction, Vector3D eye, double width, double height, int amtOfWidthPixels, int amtOfHeightPixels)
        {
            Direction = direction;
            Eye = eye;
            Height = height;
            Width = width;
            AmtOfHeightPixels = amtOfHeightPixels;
            AmtOfWidthPixels = amtOfWidthPixels;
            GenerateCameraVectors();
        }
    }
}
