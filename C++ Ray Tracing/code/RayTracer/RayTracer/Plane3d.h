#pragma once
#include "Object3d.h"
#include "Point3d.h"
#include "Vector3d.h"
#include "ShaderBase.h"

namespace RayTracer
{
	class Plane3d : public Object3d
	{
	private:
		const Vector3d normal;
		const Point3d position;
	public:
		ObjectType objectType(){ return PLANE; };
		Plane3d(Point3d point, Vector3d normal, vector<ShaderBase*> shaders);
		Plane3d(Point3d point, Vector3d normal, vector<ShaderBase*> shaders,Material material);

		Vector3d CalculateNormal(Point3d); // normal based on position always the same for a plane.
		Point3d CalculateCollisionPosition(Line3d);
		RayHit CalculateCollision(Line3d line);
	};

}

