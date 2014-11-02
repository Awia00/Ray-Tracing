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
            if (Reflectivity == 0) return true;
            else
            {
                return false;
            }
        }

        public bool IsRefractive()
        {
            if (Reflectivity == 0) return true;
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

        public Color CalculateColor(IList<ILight> lightsThatHitsSurface, Vector3D NormalVector3D, Vector3D RayVector3D)
        {
            Color shaderColor ;
            if (lightsThatHitsSurface.Count == 0) throw new Exception();
            foreach (var iLight in lightsThatHitsSurface)
            {
                if (iLight is AmbientLight)
                {
                    shaderColor = iLight.Color;
                    lightsThatHitsSurface.Remove(iLight);
                    break;
                }
            }
            foreach (var iLight in lightsThatHitsSurface)
            {
                
            }
            shaderColor = new Color();
            return shaderColor;
        }
    }
}
