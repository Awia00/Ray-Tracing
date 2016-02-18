#pragma once
#include "Stdafx.h"
#include "Line3d.h"
#include "Vector3d.h"
#include "Color.h"
#include "LightBase.h"
#include "ShaderBase.h"
#include <vector>

using namespace std;

#include "Material.h"

namespace RayTracer{
	struct RayHit {
		bool success;
		Point3d point;
		Vector3d normal;

		RayHit() :success(false), point(Point3d()), normal(Vector3d()) {}
		RayHit(Point3d point, Vector3d normal) :success(true), point(point), normal(normal) {}
	};

	enum ObjectType
	{
		PLANE,
		NONPLANE
	};

	class Object3d
	{
	public:
		const vector<ShaderBase*> shaders;
		const Material material;
		Object3d(vector<ShaderBase*> shaders) : shaders(shaders), material(Material()) { }
		Object3d(vector<ShaderBase*> shaders, Material material) : shaders(shaders), material(material) {  }

		virtual Vector3d CalculateNormal(Point3d) = 0; 
		virtual Point3d CalculateCollisionPosition(Line3d) = 0;
		virtual RayHit CalculateCollision(Line3d) = 0;
		virtual ObjectType objectType() = 0;

		ColorIntern shadeThis(Vector3d eyeVector, Vector3d normalToSurface, Point3d pointOnObject, vector<LightBase*> lights) {
			ColorIntern colorToReturn = ColorIntern();

			for each (ShaderBase* shader in shaders) {
				colorToReturn = ColorIntern::blendAddition(colorToReturn, shader->shade(eyeVector, normalToSurface, pointOnObject, lights));
			}

			return colorToReturn;
		}
	};
}
