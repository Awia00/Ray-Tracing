using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingModel.Model.Objects3D
{
    public class Line3D
    {
        private static Random _random = new Random();
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

        public IList<Line3D> Twist(int amountOfRays, double twistAmount)
        {
            var temp = new List<Line3D>();
            for (int i = 0; i < amountOfRays; i++)
            {
                var tempDirection = new Vector3D(
                    DirectionVector.X + twistAmount * RandomTwistAmt(),
                    DirectionVector.Y + twistAmount * RandomTwistAmt(), 
                    DirectionVector.Z + twistAmount * RandomTwistAmt());
                temp.Add(new Line3D(PositionVector, tempDirection.Normalize()));
            }
            temp.Add(this);
            return temp;
        }

        private double RandomTwistAmt()
        {
            return _random.NextDouble() * 2 - 1;
        }
    }
}
