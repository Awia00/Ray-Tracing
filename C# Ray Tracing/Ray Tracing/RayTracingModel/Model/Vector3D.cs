using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingModel.Model
{
    public class Vector3D
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public readonly double Length;

        /// <summary>
        /// Empty vector. Used for ambient lights
        /// </summary>
        public Vector3D()
        {
            
        }
        /// <summary>
        /// True vector.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            Length = Math.Sqrt(Math.Pow(X, 2)+Math.Pow(Y, 2)+Math.Pow(Z, 2));
        }

        public Vector3D(Vector3D startPosition, Vector3D endPosition)
        {
            X = endPosition.X - startPosition.X;
            Y = endPosition.Y - startPosition.Y;
            Z = endPosition.Z - startPosition.Z;
            Length = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
        }

        public Vector3D Normalize()
        {
            return new Vector3D(X/Length, Y/Length, Z/Length);
        }

        public Vector3D VectorTimesDouble(double scalar)
        {
            return new Vector3D(X * scalar, Y * scalar, Z * scalar);
        }

        public Vector3D VectorNegation()
        {
            return new Vector3D(-X,-Y,-Z);
        }

        // ------- Static methods -------

        public static Vector3D Addition(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);
        }

        public static Vector3D Subtraction(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D(vector1.X - vector2.X, vector1.Y - vector2.Y, vector1.Z - vector2.Z);
        }

        public static Vector3D Multiplication(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D(vector1.X * vector2.X, vector1.Y * vector2.Y, vector1.Z * vector2.Z);
        }

        public static double DotProdukt(Vector3D vector1, Vector3D vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
        }

        public static double GetCosV(Vector3D vector1, Vector3D vector2)
        {
            return (Vector3D.DotProdukt(vector1, vector2)) / (vector1.Length * vector2.Length);
        }
    }
}
