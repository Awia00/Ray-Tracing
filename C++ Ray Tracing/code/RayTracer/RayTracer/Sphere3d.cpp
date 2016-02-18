#include "stdafx.h"
#include "Sphere3d.h"
#include <cmath>

#include <iostream>

namespace RayTracer{


	Sphere3d::Sphere3d(Point3d center, float _radius) : centerPosition(center), radius(_radius), Object3d(vector<ShaderBase*>()) {
	}

	Sphere3d::Sphere3d(Point3d center, float _radius, vector<ShaderBase*> shaders) : centerPosition(center), radius(_radius), Object3d(shaders)
	{
	}

	Sphere3d::Sphere3d(Point3d center, float _radius, vector<ShaderBase*> shaders, Material material) : centerPosition(center), radius(_radius), Object3d(shaders,material)
	{
	}

	// Returns (for now) vector that is normal to the sphere through point.
	// The returned vector is normalized
	Vector3d Sphere3d::CalculateNormal(Point3d pointAtSphere)
	{
		float x = pointAtSphere.x - centerPosition.x;
		float y = pointAtSphere.y - centerPosition.y;
		float z = pointAtSphere.z - centerPosition.z;

		Vector3d initial = Vector3d(x, y, z); // TODO: use vector3ds constructor instead.
		Vector3d normalized = Vector3d::normalize(initial);

		return normalized;
	}

	Point3d Sphere3d::CalculateCollisionPosition(Line3d line)
	{
		RayHit hit = CalculateCollision(line);
		return hit.point;
	}

	RayHit Sphere3d::CalculateCollision(Line3d line) {
		// Notice, that there may be two intersections here, but only the "first" is returned

		//	As this method may be given a Line3d instance that does not originate from (0,0,0) a translation is made
		//	to ease the calculation

		// Move circle from spot to origin; hence we can act as if line is originating from origo. 
		Vector3d translatedCenter = Vector3d(centerPosition, line.position);


		// Do calculations
		float a = Vector3d::dotProduct(line.direction, line.direction);		// What? This will always return 1.0, if line.direction is normalized
		float b = 2.0f*(Vector3d::dotProduct(line.direction, Vector3d::negate(translatedCenter)));
		float c = Vector3d::dotProduct(translatedCenter, translatedCenter) - (radius*radius);

		float d = pow(b, 2.0f) - 4.0f*a*c;

		// For now, arbitrarily chosen threshold for "0"
		float threshold = 0.001f;
		if(d > 0.0f) 
		{
			// Two solutions:
		
			float tValue1 = (-1.0f*b + sqrt(pow(b, 2) - 4.0f*a*c)) / (2.0f*a);
			float tValue2 = (-1.0f*b - sqrt(pow(b, 2) - 4.0f*a*c)) / (2.0f*a);

			float lowestTValue;
			// The lowest, nonnegative tValue corresponds to the first intersection
			if(tValue1 > 0.0f && tValue2 > 0.0f && tValue1 < tValue2)
			{
				lowestTValue = tValue1;
			}
			else if (tValue1 > 0.0f && tValue2 > 0.0f && tValue2 < tValue1)
			{
				lowestTValue = tValue2;
			}
			else if (tValue1 < 0.0f && tValue2 > 0.0f)
			{
				lowestTValue = tValue2;
			}
			else if (tValue2 < 0.0f && tValue1 > 0.0f)
			{
				lowestTValue = tValue1;
			}
			else
			{
				// Both are negative, and hence no valid solution
				return RayHit();
			}


			// First solution: (-(b)+sqrt(D))/(2*a) and Make up for initial translation
			Vector3d intersect = Vector3d::add(Vector3d::multiply(line.direction, lowestTValue), Vector3d::fromPoint(line.position));
			Point3d point = Point3d(intersect.x, intersect.y, intersect.z);

			return RayHit(point,CalculateNormal(point));
		} 
		else if(d > 0.0 && d < threshold) 
		{
			// One solution: (-b)/(2*a)
			float tValue = (-1.0f*b) / (2.0f*a);

			if (tValue < 0.0f)
			{
				return RayHit();
			}

			// Corresponding point on line with given t and Make up for initial translation
			Vector3d intersect = Vector3d::add(Vector3d::multiply(line.direction, tValue), Vector3d::fromPoint(line.position));
			Point3d point = Point3d(intersect.x, intersect.y, intersect.z);

			return RayHit(point, CalculateNormal(point));
		} 
		else 
		{
			// No solutions: Return empty array
			return RayHit();
		}
	}
}
