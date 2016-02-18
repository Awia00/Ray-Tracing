#include "stdafx.h"
#include "AmbientShader.h"
#include "LightBase.h"

namespace RayTracer{
	AmbientShader::AmbientShader() : objectColor(ColorIntern(0, 0, 0, 0))
	{
	}

	AmbientShader::AmbientShader(ColorIntern objectColor) : objectColor(objectColor)
	{

	}

	ColorIntern AmbientShader::shade(Vector3d eyeVector, Vector3d normalToSurface, Point3d pointOnObject, vector<LightBase*> lights)
	{
		ColorIntern colorToReturn = ColorIntern();

		for (vector<LightBase*>::iterator it = lights.begin(); it != lights.end(); ++it) {
			LightBase* light = *it;
			if ((light->getLightType()) == AMBIENT)
			{
				ColorIntern lightOnObjectColor = ColorIntern::blendMultiply(objectColor, light->getLightColor());

				ColorIntern ambientColor = ColorIntern::intensifyColor(lightOnObjectColor, light->GetIntensityOnPoint(pointOnObject));
				colorToReturn = ColorIntern::blendAddition(colorToReturn, ambientColor);
			}
		}
		return colorToReturn;
	}
}

