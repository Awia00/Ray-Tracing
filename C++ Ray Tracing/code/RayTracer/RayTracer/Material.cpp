#include "stdafx.h"
#include "Material.h"

namespace RayTracer
{
	Material::Material() : reflectiveness(0.0f), transparency(0.0f), materialRefractionIndex(1.0f)
	{

	}

	Material::Material(float reflectiveness) : reflectiveness(reflectiveness), transparency(0.0f), materialRefractionIndex(1.0f)
	{

	}

	Material::Material(float reflectiveness, float transparency, float materialRefractionIndex) : reflectiveness(reflectiveness), transparency(transparency), materialRefractionIndex(materialRefractionIndex)
	{

	}
}