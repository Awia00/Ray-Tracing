using RayTracingModel.Model.Objects3D;

namespace RayTracingModel.Model.Cameras
{
    public class SimpleStereoCamera : ICamera
    {
        public Vector3D Direction { get; set; }
        public Vector3D LeftEye { get; set; }
        public Vector3D RightEye { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int AmtOfHeightPixels { get; set; }
        public int AmtOfWidthPixels { get; set; }
        public double DistanceBetweenEyes { get; set; }

        private Line3D[,] _cameraRays;
        public Line3D[,] GenerateCameraVectors()
        {
            if (_cameraRays == null)
            {
                var tempCameraVectors = new Line3D[AmtOfWidthPixels, AmtOfHeightPixels];
                double pixelWidth = Width / AmtOfWidthPixels*2;
                double pixelHeight = Height / AmtOfHeightPixels;
                for (int i = 0; i < AmtOfWidthPixels/2; i++)
                {
                    for (int j = 0; j < AmtOfHeightPixels; j++)
                    {
                        var tempDirectionVector = new Vector3D(
                            (-Width / 2 + i * pixelWidth),
                            (Direction.Length),
                            (-Height / 2 + j * pixelHeight));
                        tempCameraVectors[i, j] = new Line3D(LeftEye, tempDirectionVector.Normalize());
                    }
                }
                for (int i = 0; i < AmtOfWidthPixels / 2; i++)
                {
                    for (int j = 0; j < AmtOfHeightPixels; j++)
                    {
                        var tempDirectionVector = new Vector3D(
                            ((-Width) / 2 + i * pixelWidth),
                            (Direction.Length),
                            (-Height / 2 + j * pixelHeight));
                        int secondImageWidthPixel = AmtOfWidthPixels / 2 + i;
                        tempCameraVectors[secondImageWidthPixel, j] = new Line3D(RightEye, tempDirectionVector.Normalize());
                    }
                }
                _cameraRays = tempCameraVectors;
            }
            return _cameraRays;
        }

        public SimpleStereoCamera(Vector3D direction, Vector3D leftEye, Vector3D rightEye, double width, double height, int amtOfWidthPixels, int amtOfHeightPixels)
        {
            Direction = direction;
            LeftEye = leftEye;
            RightEye = rightEye;
            Height = height;
            Width = width;
            AmtOfHeightPixels = amtOfHeightPixels;
            AmtOfWidthPixels = amtOfWidthPixels;
            GenerateCameraVectors();
        }
    }
}
