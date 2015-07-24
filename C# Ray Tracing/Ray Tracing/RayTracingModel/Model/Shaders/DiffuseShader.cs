using System;
using System.Collections.Generic;
using System.Drawing;
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
            return false;
        }

        public bool IsRefractive()
        {
            if (Refractivity > 0) return true;
            return false;
        }

        public double RefractionIndex { get; set; }

        public Color DiffuseColor { get; set; }

        public DiffuseShader(Color diffuseColor, double reflectivity, double refractivity, double refractionIndex)
        {
            DiffuseColor = diffuseColor;
            Reflectivity = reflectivity;
            Refractivity = refractivity;
            RefractionIndex = refractionIndex;
            CheckInvariants();
        }

        private void CheckInvariants()
        {
            if (Reflectivity < 0 || Reflectivity < 0) throw new Exception("Neither reflectivity or refractivity must be beneath 0");
            if (Reflectivity > 1 || Reflectivity > 1) throw new Exception("Neither reflectivity or refractivity must be over 1");
        }

        public Color CalculateColor(IList<ILight> lightsThatHitsSurface, Vector3D normalVector3D, Vector3D rayVector3D, Vector3D collisionPositionVector3D)
        {
            if (lightsThatHitsSurface.Count == 0) throw new Exception();

            Color baseColor = new Color();
            Color ambientColor = GenerateAmbientColor(lightsThatHitsSurface);
            Color diffuseColor = GenerateDiffuseColor(lightsThatHitsSurface, normalVector3D, collisionPositionVector3D);

            baseColor = ColorToolbox.ColorToolbox.BlendAddition(baseColor, ambientColor);
            baseColor = ColorToolbox.ColorToolbox.BlendAddition(baseColor, diffuseColor);

            return baseColor;
        }

        private Color GenerateAmbientColor(IList<ILight> lightsThatHitsSurface)
        {
            foreach (var light in lightsThatHitsSurface)
            {
                if (light is AmbientLight)
                {
                    lightsThatHitsSurface.Remove(light);
                    return ColorToolbox.ColorToolbox.ColorIntensify(light.Color, light.Intensity);
                }
            }
            return new Color();
        }

        private Color GenerateDiffuseColor(IList<ILight> lightsThatHitsSurface, Vector3D normalVector3D, Vector3D collisionPositionVector3D)
        {
            double intensity = 0;
            foreach (var light in lightsThatHitsSurface)
            {
                intensity += light.Intensity * (Math.Max(0, Vector3D.DotProdukt(normalVector3D.VectorNegation(), light.CalculateLightDirectionOnPosition(collisionPositionVector3D))));
            }
            return ColorToolbox.ColorToolbox.ColorIntensify(DiffuseColor, intensity);
        }
    }
}
