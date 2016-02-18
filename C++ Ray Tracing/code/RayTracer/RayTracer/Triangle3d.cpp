#include "stdafx.h"
#include "Triangle3d.h"

namespace RayTracer {
	Triangle3d::Triangle3d(Point3d a, Point3d b, Point3d c, vector<ShaderBase*> shaders)
		: a(a), b(b), c(c), u(Vector3d(b, a)), v(Vector3d(c, a)),
			normal(Vector3d::crossProduct(Vector3d(b, a), Vector3d(c, a))), Object3d(shaders) 
	{

	}

	Triangle3d::Triangle3d(Point3d a, Point3d b, Point3d c, vector<ShaderBase*> shaders, Material material) :
		a(a), b(b), c(c), u(Vector3d(b, a)), v(Vector3d(c, a)),
		normal(Vector3d::crossProduct(Vector3d(b, a), Vector3d(c, a))), Object3d(shaders,material)
	{

	}

	Vector3d Triangle3d::CalculateNormal(Point3d point) {
		return normal;
	}
	float abs(float f) {
		return f < 0 ? -f : f;
	}
	Point3d Triangle3d::CalculateCollisionPosition(Line3d line) {
		RayHit hit = CalculateCollision(line);
		return hit.point;
	}
	// Source http://geomalgorithms.com/a06-_intersect-2.html#intersect3D_RayTriangle%28%29
	RayHit Triangle3d::CalculateCollision(Line3d line) {
		float     r, a, b;              // params to calc ray-plane intersect

		Vector3d dir = line.direction;              // ray direction vector
		Vector3d w0 = Vector3d(line.position, this->a);
		a = -Vector3d::dotProduct(normal, w0);
		b = Vector3d::dotProduct(normal, dir);
		if(abs(b) < 0.000001f) {     // ray is  parallel to triangle plane
			return RayHit(); // Disjoint. no intersect
		}

		// get intersect point of ray with triangle plane
		r = a / b;
		if(r < 0.0)                    // ray points away from triangle
			return RayHit();                   // => no intersect
		// for a segment, also test if (r > 1.0) => no intersect

		Point3d I = line.getPositionAlongLine(r);            // intersect point of ray and plane

		// is I inside T?
		float    uu, uv, vv, wu, wv, D;
		uu = Vector3d::dotProduct(u, u);
		uv = Vector3d::dotProduct(u, v);
		vv = Vector3d::dotProduct(v, v);
		Vector3d w = Vector3d(I, this->a);
		wu = Vector3d::dotProduct(w, u);
		wv = Vector3d::dotProduct(w, v);
		D = uv * uv - uu * vv;

		// get and test parametric coords
		float s, t;
		s = (uv * wv - vv * wu) / D;
		if(s < 0.0 || s > 1.0)         // I is outside T
			return RayHit();
		t = (uv * wu - uu * wv) / D;
		if(t < 0.0 || (s + t) > 1.0)  // I is outside T
			return RayHit();

		return RayHit(I,normal);                       // I is in T
	}
}
