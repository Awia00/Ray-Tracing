#pragma once
#include "Object3d.h"
#include "Point3d.h"
#include "Vector3d.h"
#include "ShaderBase.h"

namespace RayTracer {
	class Triangle3d : public Object3d {
	private:
	public:
		Point3d a, b, c;
		Vector3d u, v, normal;
		Triangle3d(Point3d a, Point3d b, Point3d c, vector<ShaderBase*> shaders);
		Triangle3d(Point3d a, Point3d b, Point3d c, vector<ShaderBase*> shaders, Material material);
		ObjectType objectType(){ return NONPLANE; };

		Vector3d CalculateNormal(Point3d point);
		Point3d CalculateCollisionPosition(Line3d line);
		RayHit CalculateCollision(Line3d line);
	};
}

