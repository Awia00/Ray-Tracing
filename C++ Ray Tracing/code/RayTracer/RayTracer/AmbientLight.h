#pragma once
#include "LightBase.h"
#include "Vector3d.h"
namespace RayTracer
{
	class AmbientLight : public LightBase
	{
		float intensity;
		ColorIntern color;
	public:
		AmbientLight();
		AmbientLight(float intensity);
		AmbientLight(float intensity, ColorIntern color);
		LightBase* getCopyOfLight(float intensityFactor);

		Vector3d GetLightOnPoint(Point3d position);
		float GetIntensityOnPoint(Point3d position);
		LightType getLightType();
		ColorIntern getLightColor();
	};
}

