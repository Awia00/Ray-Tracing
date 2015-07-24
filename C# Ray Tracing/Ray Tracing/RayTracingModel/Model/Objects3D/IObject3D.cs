using RayTracingModel.Model.Shaders;

namespace RayTracingModel.Model.Objects3D
{
    public interface IObject3D
    {
        IShader Shader { get; set; }
        Vector3D CenterPositionVector3D { get; set; }
        double CalculateCollisionPosition(Line3D line3D);
        Vector3D CalculateNormVector(Vector3D positionVector3D);
    }
}
