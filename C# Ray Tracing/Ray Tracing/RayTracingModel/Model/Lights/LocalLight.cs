using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingModel.Model.Lights
{
    public class LocalLight : ILight
    {
        public double Intensity { get; set; }
        public Color Color { get; set; }

        public Vector3D Position { get; set; }

        public LocalLight(double intensity, Color color, Vector3D position)
        {
            Intensity = intensity;
            Color = color;
            Position = position;
        }

        public Vector3D CalculateLightDirectionOnPosition(Vector3D positionVector3D)
        {
            return Vector3D.Subtraction(positionVector3D, Position).Normalize();
        }

        public ILight Clone()
        {
            return new LocalLight(Intensity,Color,Position);
        }
    }
}
