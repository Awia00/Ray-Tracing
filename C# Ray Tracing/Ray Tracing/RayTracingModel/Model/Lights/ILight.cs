using System.Drawing;

namespace RayTracingModel.Model.Lights
{
    public interface ILight
    {
        double Intensity { get; set; }
        Color Color { get; set; }
        Vector3D CalculateLightDirectionOnPosition(Vector3D positionVector3D);
        double DistanceFromLight(Vector3D positionVector3D);
        ILight Clone();
    }
}
