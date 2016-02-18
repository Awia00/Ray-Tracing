#include "stdafx.h"
#include "Color.h"
#include <iostream>

namespace RayTracer
{
	Color::Color() : red(0), blue(0), green(0), alpha(255)
	{

	}

	Color::Color(ColorIntern color) : red(color.red), blue(color.blue), green(color.green), alpha(color.alpha)
	{
		red = sanitizeColor((int)red);
		green = sanitizeColor((int)green);
		blue = sanitizeColor((int)blue);
	}

	Color::Color(int red, int green, int blue, int alpha) : red(red), green(green), blue(blue), alpha(alpha)
	{
		if (red > 255 || green > 255 || blue > 255)
		{
			// throw exception
		}
		if (red < 0 || green < 0 || blue < 0)
		{
			// throw exception
		}
	}
	int Color::sanitizeColor(int proposedColorValue)
	{
		if (proposedColorValue > 255)
		{
			return (int)255;
		}
		else if (proposedColorValue < 0)
		{
			return (int)0;
		}

		return (int)proposedColorValue;
	}


	// Intern Color
	ColorIntern::ColorIntern() : red(0), blue(0), green(0), alpha(255)
	{

	}

	ColorIntern::ColorIntern(float red, float green, float blue, float alpha) : red(sanitizeColor(red)), green(sanitizeColor(green)), blue(sanitizeColor(blue)), alpha(alpha)
	{

	}

	ColorIntern ColorIntern::blendAddition(ColorIntern colorA, ColorIntern colorB)
	{
		int red =	sanitizeColor(colorA.red + colorB.red);
		int green = sanitizeColor(colorA.green + colorB.green);
		int blue =	sanitizeColor(colorA.blue + colorB.blue);
		return ColorIntern(red, green, blue, 255);
	}

	ColorIntern ColorIntern::blendMultiply(ColorIntern colorA, ColorIntern colorB)
	{
		float red =		sanitizeColor(colorA.red * colorB.red / 255);
		float green =	sanitizeColor(colorA.green * colorB.green / 255);
		float blue =	sanitizeColor(colorA.blue * colorB.blue / 255);
		return ColorIntern(red, green, blue, 255);
	}

	ColorIntern ColorIntern::intensifyColor(ColorIntern colorA, float multiplier)
	{
		float red =		sanitizeColor(colorA.red * multiplier);
		float green =	sanitizeColor(colorA.green * multiplier);
		float blue =	sanitizeColor(colorA.blue * multiplier);

		return ColorIntern(red, green, blue, 255);
	}

	float ColorIntern::sanitizeColor(float proposedColorValue)
	{
		if (proposedColorValue > 255.0f)
		{
			return (float)255.0f;
		}
		else if (proposedColorValue < 0.0f)
		{
			return (float)0.0f;
		}

		return (float)proposedColorValue;
	}

	// ratioAtoB must be a number between 0 and 1 
	ColorIntern ColorIntern::blendByAmount(ColorIntern colorA, ColorIntern colorB, float ratioAToB)
	{
		if (ratioAToB < 0.0 || ratioAToB > 1.0)
		{
			// TODO: Throw relevant exception
		}

		float newRed = sanitizeColor(colorA.red * ratioAToB + colorB.red *(1.0f - ratioAToB));
		float newGreen = sanitizeColor(colorA.green * ratioAToB + colorB.green *(1.0f - ratioAToB));
		float newBlue = sanitizeColor(colorA.blue * ratioAToB + colorB.blue *(1.0f - ratioAToB));

		return ColorIntern(newRed, newGreen, newBlue, 255);
	}


}

