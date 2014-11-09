using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingModel.Model.Lights
{
    public class AmbientLight : ILight
    {
        public double Intensity { get; set; }
        public Color Color { get; set; }
        public Vector3D CalculateLightDirectionOnPosition(Vector3D positionVector3D)
        {
            // find out what to do here since abient light does not have vectors.
            return new Vector3D();
        }

        public double DistanceFromLight(Vector3D positionVector3D)
        {
            return 1;
        }

        public AmbientLight(double intensity, Color color)
        {
            Intensity = intensity;
            Color = color;
        }

        public ILight Clone()
        {
            return new AmbientLight(Intensity, Color);
        }
    }
}
