#include "stdafx.h"
#include "AmbientLight.h"

namespace RayTracer
{
	AmbientLight::AmbientLight() : intensity(0.0f), color(ColorIntern(255, 255, 255, 255))
	{

	}

	AmbientLight::AmbientLight(float _intensity) : intensity(_intensity), color(ColorIntern(255,255,255,255))
	{

	}

	AmbientLight::AmbientLight(float _intensity, ColorIntern color) : intensity(_intensity), color(color)
	{

	}

	LightBase* AmbientLight::getCopyOfLight(float intensityFactor)
	{
		return new AmbientLight(intensity*intensityFactor, color);
	}

	LightType AmbientLight::getLightType()
	{
		return AMBIENT;
	}
	Vector3d AmbientLight::GetLightOnPoint(Point3d position)
	{
		return Vector3d();
	}

	float AmbientLight::GetIntensityOnPoint(Point3d position)
	{
		return intensity;
	}

	ColorIntern AmbientLight::getLightColor()
	{
		return color;
	}
}
