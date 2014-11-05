using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingModel.Model.Objects3D
{
    public class Line3D
    {
        private static Random random = new Random();
        public Vector3D PositionVector { get; private set; }
        public readonly Vector3D DirectionVector;

        public Line3D(Vector3D positionVector, Vector3D directionVector)
        {
            PositionVector = positionVector;
            DirectionVector = directionVector;
        }

        public Vector3D GetPositionAlongLine(double multiplier)
        {
            return Vector3D.Addition(PositionVector, DirectionVector.VectorTimesDouble(multiplier));
        }

        public void PushStartPositionAlongLine(double t)
        {
            PositionVector = GetPositionAlongLine(t);
        }

        public IList<Line3D> Twist(int AmountOfRays, double twistAmount)
        {
            var temp = new List<Line3D>();
            for (int i = 0; i < AmountOfRays; i++)
            {
                //var tempPos = new Vector3D(PositionVector.X + twistAmount*randomSinus(),
                //    PositionVector.Y + twistAmount*randomSinus(), PositionVector.Z + twistAmount*randomSinus());

                var tempDirection = new Vector3D(DirectionVector.X + twistAmount * randomSinus(),
                    DirectionVector.Y + twistAmount * randomSinus(), DirectionVector.Z + twistAmount * randomSinus());
                temp.Add(new Line3D(PositionVector, tempDirection.Normalize()));
            }
            temp.Add(this);
            return temp;
        }

        private double randomSinus()
        {
            return random.NextDouble() * 2-1;
        }
    }
}
