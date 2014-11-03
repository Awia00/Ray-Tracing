using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracingModel.Model.Lights;
using RayTracingModel.Model.Shaders;

namespace RayTracingModel.Model.Objects3D
{
    public class PlaneObject3D : IObject3D
    {
        public IShader Shader { get; set; }
        public Vector3D CenterPositionVector3D { get; set; }
        public Vector3D NormalVector3D { get; set; }

        public PlaneObject3D()
        {
        }
        public PlaneObject3D(IShader shader, Vector3D normalVector3D, Vector3D centerPositionVector3D)
        {
            Shader = shader;
            NormalVector3D = normalVector3D;
            CenterPositionVector3D = centerPositionVector3D;
        }

        public Color CalculateColor(IList<ILight> lightsThatHitsSurface, Vector3D positionVector3D, Vector3D rayDirection, double distance)
        {
            return Shader.CalculateColor(lightsThatHitsSurface, NormalVector3D, rayDirection, positionVector3D, distance);
        }

        public double CalculateCollisionPosition(Line3D line3D)
        {
            if (Vector3D.DotProdukt(line3D.DirectionVector, NormalVector3D) ==0) return 0;
            double t = Vector3D.DotProdukt(NormalVector3D,
                Vector3D.Subtraction(CenterPositionVector3D, line3D.PositionVector))/
                Vector3D.DotProdukt(line3D.DirectionVector, NormalVector3D);
            if (t < 0) return 0;
            return t;
        }

        public Vector3D CalculateNormVector(Vector3D positionVector3D)
        {
            return NormalVector3D;
        }
    }
}
