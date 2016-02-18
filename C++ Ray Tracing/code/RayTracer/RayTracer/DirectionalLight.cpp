#include "stdafx.h"
#include "DirectionalLight.h"

namespace RayTracer
{
	DirectionalLight::DirectionalLight() : intensity(0.0f), direction(Vector3d()), color(ColorIntern(255, 255, 255, 255))
	{

	}

	DirectionalLight::DirectionalLight(float intensity, Vector3d direction) : intensity(intensity), direction(direction), color(ColorIntern(255, 255, 255, 255))
	{

	}

	DirectionalLight::DirectionalLight(float intensity, Vector3d direction, ColorIntern color) : intensity(intensity), direction(direction), color(color)
	{

	}

	LightBase* DirectionalLight::getCopyOfLight(float intensityFactor)
	{
		return new DirectionalLight(intensity*intensityFactor, direction, color);
	}

	LightType DirectionalLight::getLightType()
	{
		return DIRECTIONAL;
	}

	Vector3d DirectionalLight::GetLightOnPoint(Point3d position)
	{
		return direction;
	}

	float DirectionalLight::GetIntensityOnPoint(Point3d position)
	{
		return intensity;
	}

	ColorIntern DirectionalLight::getLightColor()
	{
		return color;
	}
}

