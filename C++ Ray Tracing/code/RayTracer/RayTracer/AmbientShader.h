#pragma once
#include "ShaderBase.h"
#include "Color.h"
namespace RayTracer
{
	class AmbientShader :
		public ShaderBase
	{
		ColorIntern objectColor;
	public:
		AmbientShader();
		AmbientShader(ColorIntern objectColor);

		ColorIntern shade(Vector3d eyeVector, Vector3d normalToSurface, Point3d pointOnObject, vector<LightBase*> lights);
	};
}


