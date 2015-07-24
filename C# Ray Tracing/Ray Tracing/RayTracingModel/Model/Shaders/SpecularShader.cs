using System;
using System.Collections.Generic;
using System.Drawing;
using RayTracingModel.Model.Lights;

namespace RayTracingModel.Model.Shaders
{
    public class SpecularShader : IShader
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

        private void CheckInvariants()
        {
            if (Reflectivity < 0 || Reflectivity < 0) throw new Exception("Neither reflectivity or refractivity must be beneath 0");
            if (Reflectivity > 1 || Reflectivity > 1) throw new Exception("Neither reflectivity or refractivity must be over 1");
        }

        public SpecularShader(Color diffuseColor, Color specularColor, double specularComponent,double reflectivity, double refractivity, double refractionIndex)
        {
            Reflectivity = reflectivity;
            Refractivity = refractivity;
            RefractionIndex = refractionIndex;
            DiffuseColor = diffuseColor;
            SpecularColor = specularColor;
            SpecularComponent = specularComponent;
        }

        public Color DiffuseColor { get; set; }
        public Color SpecularColor { get; set; }
        public double SpecularComponent { get; set; }

        public Color CalculateColor(IList<ILight> lightsThatHitsSurface, Vector3D normalVector3D, Vector3D rayVector3D, Vector3D collisionPositionVector3D)
        {
            if (lightsThatHitsSurface.Count == 0) throw new Exception();

            Color baseColor = new Color();
            Color ambientColor = GenerateAmbientColor(lightsThatHitsSurface);
            Color diffuseColor = GenerateDiffuseColor(lightsThatHitsSurface, normalVector3D, collisionPositionVector3D);
            Color specularColor = GenerateSpecularColor(lightsThatHitsSurface, normalVector3D, rayVector3D,
                collisionPositionVector3D);

            baseColor = ColorToolbox.ColorToolbox.BlendAddition(baseColor, ambientColor);
            baseColor = ColorToolbox.ColorToolbox.BlendAddition(baseColor, diffuseColor);
            baseColor = ColorToolbox.ColorToolbox.BlendAddition(baseColor, specularColor);

            return baseColor;
        }

        private Color GenerateAmbientColor(IList<ILight> lightsThatHitsSurface )
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

        private Color GenerateSpecularColor(IList<ILight> lightsThatHitsSurface, Vector3D normalVector3D, Vector3D rayVector3D,
            Vector3D collisionPositionVector3D )
        {
            double intensity = 0;
            Vector3D negDirectionVector = rayVector3D.VectorNegation();
            foreach (var light in lightsThatHitsSurface)
            {
                Vector3D lightReflection = Vector3D.ReflectionVector(light.CalculateLightDirectionOnPosition(collisionPositionVector3D), normalVector3D);
                intensity += light.Intensity*Math.Pow(Math.Max(0,Vector3D.DotProdukt(lightReflection, rayVector3D.VectorNegation())),SpecularComponent);
            }
            return ColorToolbox.ColorToolbox.ColorIntensify(SpecularColor, intensity);
        }


    }
}
