#pragma once
#include "LightBase.h"
#include "Point3d.h"
#include <vector> 
using namespace std;


namespace RayTracer{
	class SoftLightbase
	{
	public:
		virtual vector<LightBase*> getLights() = 0;
	};
}


