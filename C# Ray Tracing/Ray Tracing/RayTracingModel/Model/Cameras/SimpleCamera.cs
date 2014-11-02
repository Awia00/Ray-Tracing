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
                for (int i = 0; i < AmtOfWidthPixels; i++)
                {
                    for (int j = 0; j < AmtOfHeightPixels; j++)
                    {
                        var tempDirectionVector = new Vector3D((-Height/2 + j*(Height/(double) AmtOfHeightPixels)),
                            (Direction.Length),
                            (-Width/2 + i*(Width/(double) AmtOfWidthPixels)));
                        tempCameraVectors[i, j] = new Line3D(Eye, tempDirectionVector);
                    }
                }
                _cameraRays = tempCameraVectors;
            }
            return _cameraRays;

        }

        public SimpleCamera(Vector3D direction, Vector3D eye, double height, double width, int amtOfHeightPixels, int amtOfWidthPixels)
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
