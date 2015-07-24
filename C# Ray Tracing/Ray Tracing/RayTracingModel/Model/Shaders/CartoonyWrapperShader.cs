using System;
using System.Collections.Generic;
using System.Drawing;
using RayTracingModel.Model.Lights;

namespace RayTracingModel.Model.Shaders
{
    public class CartoonyWrapperShader : IShader
    {
        public double Reflectivity
        {
            get { return InnerShader.Reflectivity; }
            set { InnerShader.Reflectivity = value; }
        }

        public double Refractivity
        {
            get { return InnerShader.Refractivity; }
            set { InnerShader.Refractivity = value; }
        }

        public bool IsReflective()
        {
            if (InnerShader.Reflectivity > 0) return true;
            return false;
        }

        public bool IsRefractive()
        {
            if (InnerShader.Refractivity > 0) return true;
            return false;
        }

        public double RefractionIndex
        {
            get { return InnerShader.RefractionIndex; }
            set { InnerShader.RefractionIndex = value; }
        }

        private void CheckInvariants()
        {
            if (Reflectivity < 0 || Reflectivity < 0) throw new Exception("Neither reflectivity or refractivity must be beneath 0");
            if (Reflectivity > 1 || Reflectivity > 1) throw new Exception("Neither reflectivity or refractivity must be over 1");
        }

        public CartoonyWrapperShader(Color borderColor, double amtOfBorder, IShader innerShader)
        {
            InnerShader = innerShader;
            BorderColor = borderColor;
            AmtOfBorder = amtOfBorder;
        }

        public IShader InnerShader { get; set; }
        public Color BorderColor { get; set; }
        public double AmtOfBorder { get; set; }

        public Color CalculateColor(IList<ILight> lightsThatHitsSurface, Vector3D normalVector3D, Vector3D rayVector3D, Vector3D collisionPositionVector3D)
        {
            if (lightsThatHitsSurface.Count == 0) throw new Exception();

            double cosV = Vector3D.DotProdukt(normalVector3D, rayVector3D);
            if (cosV < AmtOfBorder && cosV > -AmtOfBorder) return BorderColor;

            return InnerShader.CalculateColor(lightsThatHitsSurface,normalVector3D,rayVector3D,collisionPositionVector3D);
        }
    }
}
