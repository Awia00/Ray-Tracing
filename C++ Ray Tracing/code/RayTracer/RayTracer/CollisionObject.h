#pragma once
#include "Object3d.h"
#include "Point3d.h"

namespace RayTracer{
	class CollisionObject
	{
	public:
		CollisionObject();
		CollisionObject(Object3d*, RayHit);
		Object3d* object;
		RayHit hit;
		bool isReal;
	};
}


