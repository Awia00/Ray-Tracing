#pragma once
#include "LightBase.h"
namespace RayTracer
{
	class DirectionalLight : public LightBase
	{
		float intensity;
		Vector3d direction;
		ColorIntern color;
	public:
		DirectionalLight();
		DirectionalLight(float intensity, Vector3d direction);
		DirectionalLight(float intensity, Vector3d direction, ColorIntern color);
		LightBase* getCopyOfLight(float intensityFactor);

		Vector3d GetLightOnPoint(Point3d position);
		float GetIntensityOnPoint(Point3d position);
		LightType getLightType();
		ColorIntern getLightColor();
	};
}


