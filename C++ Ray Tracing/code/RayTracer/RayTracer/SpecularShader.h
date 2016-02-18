#pragma once
#include "ShaderBase.h"
namespace RayTracer
{
	class SpecularShader : public ShaderBase
	{
		ColorIntern specularColor;
		float kExpValue;

	public:
		SpecularShader();
		SpecularShader(ColorIntern specularColor, float kExpValue);
		ColorIntern shade(Vector3d eyeVector, Vector3d normalToSurface, Point3d pointOnObject, vector<LightBase*> lights);
	};
}


