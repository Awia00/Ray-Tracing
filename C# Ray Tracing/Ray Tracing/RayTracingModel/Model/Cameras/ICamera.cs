using RayTracingModel.Model.Objects3D;

namespace RayTracingModel.Model.Cameras
{
    interface ICamera
    {
        Vector3D Direction { get; set; }
        double Height { get; set; }
        double Width { get; set; }
        int AmtOfHeightPixels { get; set; }
        int AmtOfWidthPixels { get; set; }
        Line3D[,] GenerateCameraVectors();
    }
}
