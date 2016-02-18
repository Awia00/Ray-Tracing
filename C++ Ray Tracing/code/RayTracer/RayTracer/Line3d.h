#pragma once
#include "Vector3d.h"
#include <vector>
#include <stdio.h>
#include <stdlib.h>     /* srand, rand */
#include <time.h> 
#include <random>

using namespace std;

namespace RayTracer{
	
	// Line3d should be thought of as a parametrization: (x(t),y(t),z(t)) = (x0,y0,z0) + t*(x1,y2,z2)
	class Line3d
	{
	private: 
	public:
		const Point3d position;
		const Vector3d direction;
		
		Line3d(); // empty constructor
		Line3d(Point3d, Vector3d);

		Point3d getPositionAlongLine(float);
		Line3d pushStartAlongLine(float);

		vector<Line3d> getTwistedLines(int amountOfLines, float twistAmount);
	};
}
