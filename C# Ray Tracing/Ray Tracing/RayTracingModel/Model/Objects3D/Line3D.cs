using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingModel.Model.Objects3D
{
    class Line3D
    {
        public readonly Vector3D PositionVector;
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
    }
}
