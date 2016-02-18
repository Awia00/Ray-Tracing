#pragma once

#include "Point3d.h"

namespace RayTracer{
	// The Vector3d class sh
	class Vector3d
	{
	public:
		// Denotes either a) a point in space (in which case length must be 0) or b) a vector with a direction
		float x, y, z, length;
		Vector3d(); // the empty constructor.
		Vector3d(float, float, float);	// This represents a) a point or b) a vector
		Vector3d(Point3d endPoint, Point3d startPoint);	// This represents a parametrization: (x,y,z) = (x0,y0,z0) + t*(x1,y1,z1) <- Nope, Wrong! 

		static Vector3d fromPoint(Point3d);

		// Math
		static Vector3d add(Vector3d, Vector3d);
		static Vector3d subtract(Vector3d, Vector3d);
		static Vector3d multiply(Vector3d, float);
		static Vector3d divide(Vector3d, float);

		// Vector math
		static Vector3d normalize(Vector3d);
		static Vector3d negate(Vector3d);
		static float dotProduct(Vector3d, Vector3d); // cosine when the vectors are normalized
		static float sineToAngle(Vector3d, Vector3d);
		static float cosineToAngle(Vector3d, Vector3d);
		static Vector3d crossProduct(Vector3d lhs, Vector3d rhs);
		static float distance(Vector3d, Vector3d); 

		// Other math
		static Vector3d reflectionVector(Vector3d normal, Vector3d incoming);
		static Vector3d refractionVector(Vector3d normal, Vector3d incoming, float refractionIndexFromMaterial, float refractionIndexToMaterial);
		static float reflectanceContributionOfRefraction(Vector3d normal, Vector3d incoming, float refractionIndexFromMaterial, float refractionIndexToMaterial);
		static bool isSameDirection(Vector3d, Vector3d);
	};
}
