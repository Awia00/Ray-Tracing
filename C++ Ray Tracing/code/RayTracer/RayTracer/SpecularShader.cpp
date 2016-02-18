#include "stdafx.h"
#include "SpecularShader.h"

namespace RayTracer
{
	SpecularShader::SpecularShader() : specularColor(ColorIntern(0, 0, 0, 0)), kExpValue(kExpValue)
	{

	}

	SpecularShader::SpecularShader(ColorIntern specularColor, float kExpValue) : specularColor(specularColor), kExpValue(kExpValue)
	{

	}

	ColorIntern SpecularShader::shade(Vector3d eyeVector, Vector3d normalToSurface, Point3d pointOnObject, vector<LightBase*> lights)
	{
		ColorIntern colorToReturn = ColorIntern();

		for (vector<LightBase*>::iterator it = lights.begin(); it != lights.end(); ++it) 
		{
			LightBase* light = *it;
			ColorIntern lightOnObjectColor = ColorIntern::blendMultiply(specularColor, light->getLightColor());
			if ((light->getLightType()) != AMBIENT)
			{
				Vector3d normalNormalized = Vector3d::normalize(normalToSurface);
				Vector3d lightIncommingNormalized = Vector3d::normalize(light->GetLightOnPoint(pointOnObject));
				Vector3d eyeVectorNormalized = Vector3d::negate(Vector3d::normalize(eyeVector));
				Vector3d reflectionVector = Vector3d::reflectionVector(normalNormalized, lightIncommingNormalized);
				
				float dotP = Vector3d::dotProduct(eyeVectorNormalized, reflectionVector) < 0 ? 0 : Vector3d::dotProduct(eyeVectorNormalized, reflectionVector);
				float intensity = pow(dotP, kExpValue) * light->GetIntensityOnPoint(pointOnObject);

				ColorIntern specular = ColorIntern::intensifyColor(lightOnObjectColor, intensity);
				colorToReturn = ColorIntern::blendAddition(colorToReturn, specular);
			}
		}
		return colorToReturn;
	}
}

