using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color_Toolbox;
using RayTracingModel.Model.Lights;

namespace RayTracingModel.Model.Shaders
{
    public class DiffuseShader : IShader
    {
        public double Reflectivity { get; set; }
        public double Refractivity { get; set; }
        public bool IsReflective()
        {
            if (Reflectivity > 0) return true;
            else
            {
                return false;
            }
        }

        public bool IsRefractive()
        {
            if (Reflectivity > 0) return true;
            else
            {
                return false;
            }
        }

        public Color DiffuseColor { get; set; }

        public DiffuseShader(Color diffuseColor, double reflectivity, double refractivity)
        {
            DiffuseColor = diffuseColor;
            Reflectivity = reflectivity;
            Refractivity = refractivity;
            CheckInvariants();
        }

        private void CheckInvariants()
        {
            if (Reflectivity < 0 || Reflectivity < 0) throw new Exception("Neither reflectivity or refractivity must be beneath 0");
            if (Reflectivity > 1 || Reflectivity > 1) throw new Exception("Neither reflectivity or refractivity must be over 1");
        }

        public Color CalculateColor(IList<ILight> lightsThatHitsSurface, Vector3D normalVector3D, Vector3D rayVector3D,
            Vector3D collisionPositionVector3D, double distance)
        {
            if (lightsThatHitsSurface.Count == 0) throw new Exception();

            distance = Math.Max(Math.Pow(distance / 30, 1.1), 1);

            Color baseColor = new Color();
            Color ambientColor = GenerateAmbientColor(lightsThatHitsSurface, distance);
            Color diffuseColor = GenerateDiffuseColor(lightsThatHitsSurface, normalVector3D, collisionPositionVector3D, distance);

            baseColor = ColorToolbox.BlendAddition(baseColor, ambientColor);
            baseColor = ColorToolbox.BlendAddition(baseColor, diffuseColor);

            return baseColor;
        }

        private Color GenerateAmbientColor(IList<ILight> lightsThatHitsSurface, double distance)
        {
            foreach (var light in lightsThatHitsSurface)
            {
                if (light is AmbientLight)
                {
                    lightsThatHitsSurface.Remove(light);
                    return ColorToolbox.ColorIntensify(light.Color, light.Intensity / distance);
                }
            }
            return new Color();
        }

        private Color GenerateDiffuseColor(IList<ILight> lightsThatHitsSurface, Vector3D normalVector3D, Vector3D collisionPositionVector3D, double distance)
        {
            double intensity = 0;
            foreach (var light in lightsThatHitsSurface)
            {
                intensity += light.Intensity * (Math.Max(0, Vector3D.DotProdukt(normalVector3D.VectorNegation(), light.CalculateLightDirectionOnPosition(collisionPositionVector3D))));
            }
            return ColorToolbox.ColorIntensify(DiffuseColor, intensity / distance);
        }
    }
}
