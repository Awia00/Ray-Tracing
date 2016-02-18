#pragma once
#include "Object3d.h"
#include "Triangle3d.h"
#include "Point3d.h"
#include "Vector3d.h"
#include "ShaderBase.h"
#include "Sphere3d.h"
#include <vector>

namespace RayTracer {
	class Mesh3d : public Object3d {
	private:
		const Point3d pos;
		const vector<Triangle3d*> triangles;
		Sphere3d* boundingSphere;
	public:
		Mesh3d(Point3d pos, vector<Triangle3d*> triangles, vector<ShaderBase*> shaders, Material material);
		ObjectType objectType(){ return NONPLANE; };

		Vector3d CalculateNormal(Point3d point);
		Point3d CalculateCollisionPosition(Line3d line);
		RayHit CalculateCollision(Line3d line);
	};
}