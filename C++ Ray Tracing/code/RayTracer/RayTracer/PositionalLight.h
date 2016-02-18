#pragma once
#include "LightBase.h"
namespace RayTracer
{
	class PositionalLight : public LightBase
	{
		Point3d centerPosition;	// Denotes center of lightsource
		float intensity, fallOff;
		ColorIntern color;

	public:
		PositionalLight();
		PositionalLight(float intensity, Point3d position);
		PositionalLight(float intensity, Point3d position, float fallOff);
		PositionalLight(float intensity, Point3d position, float fallOff, ColorIntern color);
		LightBase* getCopyOfLight(float intensityFactor);

		Vector3d GetLightOnPoint(Point3d position);
		float GetIntensityOnPoint(Point3d position);
		LightType getLightType();
		ColorIntern getLightColor();
	};
}

