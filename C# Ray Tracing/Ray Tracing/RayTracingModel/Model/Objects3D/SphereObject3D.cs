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
    public class SphereObject3D : IObject3D
    {
        public IShader Shader { get; set; }
        public Vector3D CenterPositionVector3D { get; set; }
        public double Radius { get; set; }

        public Color CalculateColor(IList<ILight> lightsThatHitsSurface, Vector3D positionVector3D)
        {
            return Shader.CalculateColor(lightsThatHitsSurface, positionVector3D);
        }

        public SphereObject3D(IShader shader, Vector3D centerPositionVector3D, double radius)
        {
            Shader = shader;
            CenterPositionVector3D = centerPositionVector3D;
            Radius = radius;
        }

        public double CalculateCollisionPosition(Line3D line3D)
        {
            Vector3D rayVector = line3D.DirectionVector;
            Vector3D rayDot = line3D.PositionVector;
            // the Vector coordinats
            double vX = rayVector.X;
            double vY = rayVector.Y;
            double vZ = rayVector.Z;

            // a Position on the Ray
            double pX = rayDot.X;
            double pY = rayDot.Y;
            double pZ = rayDot.Z;

            double cX = CenterPositionVector3D.X;
            double cY = CenterPositionVector3D.Y;
            double cZ = CenterPositionVector3D.Z;

            //calculate a, b, c
            //double a = Math.Pow((vX - pX), 2) + (Math.Pow((vY - pY), 2)) + (Math.Pow((vZ - pZ), 2));
            //double b = 2 * ((vX - pX) * (pX - cX) + (vY - pY) * (pY - cY) + (vZ - pZ) * (pZ - cZ));
            //double c = cX * cX + cY * cY + cZ * cZ + pX * pX + pY * pY + pZ * pZ - (2 * (cX * pX + cY * pY + cZ * pZ)) - Radius * Radius;

            // second method
            //a = Math.pow((pX-cX),2) + Math.pow((pY-cY),2) + Math.pow((pZ-cZ),2) - radius*radius;//(vX * vX + vY * vY + vZ * vZ);
            //c = Math.pow((pX-vX),2) + Math.pow((pY-vY),2) + Math.pow((pZ-vZ),2);
            //b = Math.pow((vX-cX),2) + Math.pow((vY-cY),2) + Math.pow((vZ-cZ),2) -a-c- radius*radius;

            double a = vX*vX + vY*vY + vZ*vZ;
            double b = 2*(pX*vX + pY*vY + pZ*vZ - vX*cX - vY*cY - vZ*cZ);
            double c = pX * pX - 2 * pX * cX + cX * cX + pY * pY - 2 * pY * cY + cY * cY + pZ * pZ - 2 * pZ * cZ + cZ * cZ - Radius * Radius;


            // calculate d
            double d = b * b - 4 * a * c;
            // test d for value
            if (d > 0)
            {
                double y = Math.Sqrt(d);
                double t1 = b > 0 ? (-b - y) / (2.0 * a) : (-b + y) / (2 * a);
                double t2 = c / (t1 * a);
                //System.out.println(t1 + " " + t2);
                if (t1 > t2)
                {
                    return t2;
                }
                else
                    return t1;
            }
            else if (d == 0)
            {
                return (-b) / (2 * a);
            }
            //calculate t
            // find lowest value of t 
            else return 0.0;
        }

        public Vector3D CalculateNormVector(Vector3D positionVector3D)
        {
            return new Vector3D(CenterPositionVector3D, positionVector3D).Normalize();
        }
    }
}
