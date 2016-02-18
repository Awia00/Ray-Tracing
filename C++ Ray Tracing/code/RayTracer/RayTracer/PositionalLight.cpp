#include "stdafx.h"
#include "PositionalLight.h"

namespace RayTracer
{
	PositionalLight::PositionalLight() : intensity(0.0f), centerPosition(Point3d()), fallOff(15.0f), color(ColorIntern(255, 255, 255, 255))
	{
	}

	PositionalLight::PositionalLight(float intensity, Point3d position) : intensity(intensity), centerPosition(position), fallOff(15.0f), color(ColorIntern(255,255,255,255))
	{

	}

	PositionalLight::PositionalLight(float intensity, Point3d position, float fallOff) : intensity(intensity), centerPosition(position), fallOff(fallOff)
	{

	}

	PositionalLight::PositionalLight(float intensity, Point3d position, float fallOff, ColorIntern color) : intensity(intensity), centerPosition(position), fallOff(fallOff), color(color)
	{

	}

	LightBase* PositionalLight::getCopyOfLight(float intensityFactor)
	{
		return new PositionalLight(intensity*intensityFactor, centerPosition, fallOff, color);
	}

	LightType PositionalLight::getLightType()
	{
		return POSITIONAL;
	}

	Vector3d PositionalLight::GetLightOnPoint(Point3d position)
	{
		// Return vector going from lightsource to position
		return Vector3d(position, centerPosition);

		// NOTE! normalizing this will lead to bugs in the shadow function, since lenght is used there.
	}

	float PositionalLight::GetIntensityOnPoint(Point3d position)
	{
		float intensityToReturn = intensity / ((Vector3d(position, centerPosition).length / fallOff));
		return intensityToReturn > intensity ? intensity : intensityToReturn;
	}

	ColorIntern PositionalLight::getLightColor()
	{
		return color;
	}
}
