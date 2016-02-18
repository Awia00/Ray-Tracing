#pragma once

#include "Vector3d.h"
#include "Color.h"

namespace RayTracer
{
	enum LightType
	{
		AMBIENT,
		DIRECTIONAL,
		POSITIONAL
	};
	class LightBase
	{
	public:
		virtual LightType getLightType()=0;
		virtual ColorIntern getLightColor() = 0;
		virtual LightBase* getCopyOfLight(float intensityFactor) = 0;
		// Position denotes the point, where the light should shine on
		virtual Vector3d GetLightOnPoint(Point3d position) = 0;
		virtual float GetIntensityOnPoint(Point3d position) = 0;
	};
}


