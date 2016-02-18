#pragma once
#include <vector>

namespace RayTracer
{
	class ColorIntern
	{
	public:
		float red, green, blue, alpha;
		ColorIntern();
		ColorIntern(float red, float green, float blue, float alpha);

		static float sanitizeColor(float proposedValue);
		static ColorIntern blendAddition(ColorIntern colorA, ColorIntern colorB);
		static ColorIntern blendMultiply(ColorIntern colorA, ColorIntern colorB);
		static ColorIntern intensifyColor(ColorIntern colorA, float multiplier);
		static ColorIntern blendByAmount(ColorIntern colorA, ColorIntern colorB, float ratioAToB);
	};

	public ref class Color
	{
	public:
		int red, green, blue, alpha;
		Color();
		Color(int red, int green, int blue, int alpha);
		Color(ColorIntern color);
		static int sanitizeColor(int proposedValue);
	};
}

