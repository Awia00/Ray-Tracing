#pragma once
#include "Object3d.h"
#include "Vector3d.h"
#include "ShaderBase.h"
#include "Point3d.h"

namespace RayTracer{
	class Sphere3d : public Object3d
	{
	public:
		const Point3d centerPosition;
		const float radius;
		ObjectType objectType(){ return NONPLANE; };
		Sphere3d(Point3d center, float _radius);
		Sphere3d(Point3d center, float _radius, vector<ShaderBase*> shaders);
		Sphere3d(Point3d center, float _radius, vector<ShaderBase*> shaders, Material material);
		
		Vector3d CalculateNormal(Point3d pointAtSphere); // normal based on position
		Point3d CalculateCollisionPosition(Line3d);
		RayHit CalculateCollision(Line3d line);
	};
}
