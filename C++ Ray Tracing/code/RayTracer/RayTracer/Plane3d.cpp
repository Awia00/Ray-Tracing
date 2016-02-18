#include "stdafx.h"
#include "Plane3d.h"

namespace RayTracer
{
	Plane3d::Plane3d(Point3d point, Vector3d normal, vector<ShaderBase*> shaders) 
		: position(point), normal(normal), Object3d(shaders)
	{
	}

	Plane3d::Plane3d(Point3d point, Vector3d normal, vector<ShaderBase*> shaders, Material material) : 
		position(point), normal(normal), Object3d(shaders,material)
	{

	}

	Vector3d Plane3d::CalculateNormal(Point3d point) {
		return normal;
	}
	Point3d Plane3d::CalculateCollisionPosition(Line3d line) {
		if(Vector3d::dotProduct(line.direction, normal) == 0) return Point3d();

		float d = (Vector3d::dotProduct(Vector3d(position, line.position), normal)) / (Vector3d::dotProduct(line.direction, normal));

		return d > 0 ? line.getPositionAlongLine(d) : Point3d();
	}
	RayHit Plane3d::CalculateCollision(Line3d line) {
		if(Vector3d::dotProduct(line.direction, normal) == 0) return RayHit();

		float d = (Vector3d::dotProduct(Vector3d(position, line.position), normal)) / (Vector3d::dotProduct(line.direction, normal));
		Point3d point = line.getPositionAlongLine(d);

		return d > 0?RayHit(point,normal):RayHit();
	}
}
