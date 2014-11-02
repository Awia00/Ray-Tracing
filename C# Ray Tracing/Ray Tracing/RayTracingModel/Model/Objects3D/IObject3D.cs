using System.Collections.Generic;
using System.Drawing;
using RayTracingModel.Model.Lights;
using RayTracingModel.Model.Shaders;

namespace RayTracingModel.Model.Objects3D
{
    public interface IObject3D
    {
        IShader Shader { get; set; }
        Vector3D CenterPositionVector3D { get; set; }
        Color CalculateColor(IList<ILight> lightsThatHitsSurface, Vector3D positionVector3D);
        double CalculateCollisionPosition(Line3D line3D);
        Vector3D CalculateNormVector(Vector3D positionVector3D);
    }
}
