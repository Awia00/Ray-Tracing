#pragma once
namespace RayTracer
{
	class Material
	{
	public:
		Material();
		Material(float reflectiveness);
		Material(float reflectiveness, float transparency, float materialRefractionIndex);
		const float reflectiveness;
		const float transparency;
		const float materialRefractionIndex;
	};
}


