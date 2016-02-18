#include "stdafx.h"
#include "BoxSoftLight.h"
#include "PositionalLight.h"

namespace RayTracer{


	BoxSoftLight::BoxSoftLight()
	{
		lights = vector<LightBase*>(0);
	}

	BoxSoftLight::BoxSoftLight(float totalIntensity, Point3d position, float fallOff, ColorIntern color, float deltaX, float deltaY, float deltaZ, int amtOfLightsX, int amtOfLightsY, int amtOfLightsZ)
	{
		int lightsTimes3 = amtOfLightsX * amtOfLightsY * amtOfLightsZ;
		float eachLightIntensity = totalIntensity / (float)(1.0f*lightsTimes3);
		lights = vector<LightBase*>(lightsTimes3);
		int index = 0;
		for (size_t i = 0; i < amtOfLightsZ; i++)
		{
			for (size_t j = 0; j < amtOfLightsY; j++)
			{
				for (size_t k = 0; k < amtOfLightsX; k++)
				{
					float x = position.x - deltaX / 2.0f + (deltaX / (float)(1.0f * amtOfLightsX)) * (float)k;
					float y = position.y - deltaY / 2.0f + (deltaY / (float)(1.0f * amtOfLightsY)) * (float)j;
					float z = position.z - deltaZ / 2.0f + (deltaZ / (float)(1.0f * amtOfLightsZ)) * (float)i;
					Point3d point = Point3d(x, y, z);
					lights[index++] = new PositionalLight(eachLightIntensity, point, fallOff, color);
				}
			}
		}
	}

	vector<LightBase*> BoxSoftLight::getLights()
	{
		return lights;
	}
}
