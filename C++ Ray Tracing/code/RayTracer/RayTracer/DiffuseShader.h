#pragma once
#include "ShaderBase.h"
namespace RayTracer{
	class DiffuseShader :
		public ShaderBase
	{
		ColorIntern objectColor;
	public:
		DiffuseShader();
		DiffuseShader(ColorIntern objectColor);

		ColorIntern shade(Vector3d eyeVector, Vector3d normalToSurface, Point3d pointOnObject, vector<LightBase*> lights);
	};

}

