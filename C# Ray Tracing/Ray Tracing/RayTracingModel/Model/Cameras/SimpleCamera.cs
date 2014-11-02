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

        public Line3D[,] GenerateCameraVectors()
        {
            var tempCameraVectors = new Line3D[AmtOfHeightPixels, AmtOfWidthPixels];
            for (int i = 0; i < AmtOfHeightPixels; i++)
            {
                for (int j = 0; j < AmtOfWidthPixels; j++)
                {
                    var tempDirectionVector = new Vector3D(-Width/2 + i*(Width/(double)AmtOfWidthPixels), 
                        Direction.Length,
                        -Height/2 + j*(Height/(double)AmtOfHeightPixels));
                    tempCameraVectors[i,j] = new Line3D(Eye, tempDirectionVector);
                }
            }
            return tempCameraVectors;

        }

        public SimpleCamera(Vector3D direction, Vector3D eye, double height, double width, int amtOfHeightPixels, int amtOfWidthPixels)
        {
            Direction = direction;
            Eye = eye;
            Height = height;
            Width = width;
            AmtOfHeightPixels = amtOfHeightPixels;
            AmtOfWidthPixels = amtOfWidthPixels;
        }
    }
}
