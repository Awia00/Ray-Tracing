#include "stdafx.h"
#include "CollisionObject.h"

namespace RayTracer{
	CollisionObject::CollisionObject() : isReal(false)
	{
	}
	CollisionObject::CollisionObject(Object3d* objectHit, RayHit hit):object(objectHit), hit(hit), isReal(true)
	{
		
	}
}

