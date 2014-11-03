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
    public class SpecularShader : IShader
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
        private void CheckInvariants()
        {
            if (Reflectivity < 0 || Reflectivity < 0) throw new Exception("Neither reflectivity or refractivity must be beneath 0");
            if (Reflectivity > 1 || Reflectivity > 1) throw new Exception("Neither reflectivity or refractivity must be over 1");
        }

        public SpecularShader(Color diffuseColor, Color specularColor, double reflectivity, double refractivity, double specularComponent)
        {
            Reflectivity = reflectivity;
            Refractivity = refractivity;
            DiffuseColor = diffuseColor;
            SpecularColor = specularColor;
            SpecularComponent = specularComponent;
        }

        public Color DiffuseColor { get; set; }
        public Color SpecularColor { get; set; }
        public double SpecularComponent { get; set; }

        public Color CalculateColor(IList<ILight> lightsThatHitsSurface, Vector3D normalVector3D, Vector3D rayVector3D,
            Vector3D collisionPositionVector3D, double distance)
        {
            if (lightsThatHitsSurface.Count == 0) throw new Exception();

            distance = Math.Pow(distance/20, 2);

            Color baseColor = new Color();
            Color ambientColor = GenerateAmbientColor(lightsThatHitsSurface,distance);
            Color diffuseColor = GenerateDiffuseColor(lightsThatHitsSurface, normalVector3D, collisionPositionVector3D,distance);
            Color specularColor = GenerateSpecularColor(lightsThatHitsSurface, normalVector3D, rayVector3D,
                collisionPositionVector3D,distance);

            baseColor = ColorToolbox.BlendAddition(baseColor, ambientColor);
            baseColor = ColorToolbox.BlendAddition(baseColor, diffuseColor);
            baseColor = ColorToolbox.BlendAddition(baseColor, specularColor);

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

        private Color GenerateSpecularColor(IList<ILight> lightsThatHitsSurface, Vector3D normalVector3D, Vector3D rayVector3D,
            Vector3D collisionPositionVector3D, double distance)
        {
            double intensity = 0;
            Vector3D negDirectionVector = rayVector3D.VectorNegation();
            foreach (var light in lightsThatHitsSurface)
            {
                Vector3D lightReflection = Vector3D.ReflectionVector(light.CalculateLightDirectionOnPosition(collisionPositionVector3D), normalVector3D);
                intensity += light.Intensity*Math.Pow(Math.Max(0,Vector3D.DotProdukt(lightReflection, rayVector3D.VectorNegation())),SpecularComponent);

                //Vector3D hVector = Vector3D.Addition(negDirectionVector, light.CalculateLightDirectionOnPosition(collisionPositionVector3D).Normalize()).Normalize();
                // intensity += light.Intensity * Math.Pow(Math.Max(0, Vector3D.DotProdukt(hVector, normalVector3D.VectorNegation())), SpecularComponent);
            }
            // todo rebuild color toolbox with safe check on intensify.
            return ColorToolbox.ColorIntensify(SpecularColor, intensity / distance);
        }


    }
}
