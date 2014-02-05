/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package raytracer;

import java.awt.Color;
import java.util.ArrayList;

/**
 *
 * @author Anders
 */
public class ShaderDiffuse implements IShader {

    private Color colorDiffuse;

    public ShaderDiffuse(Color colorDiffuse) {
        this.colorDiffuse = colorDiffuse;
    }

    @Override
    public Color getShadingColor(Collision collision, ArrayList<ILightObject> lights, Color ambientColor) {
        Color colorToReturn = colorDiffuse;
        if (lights.isEmpty()) {
            return ambientColor;
        } else {
            colorToReturn = getDiffuseShading(lights, collision.getNormal());
            // calculate the intensity coming from each light.

            colorToReturn = ColorToolbox.ColorSum(colorToReturn, ambientColor);
            return colorToReturn;
        }
    }

    private Color getDiffuseShading(ArrayList<ILightObject> lights, Vector3d normvect) {
        double intensity = 0;
        for (ILightObject light : lights) {
            intensity += light.getIntensity() * (Math.max(0, Vector3d.dotProdukt(normvect, light.getDirectionVector())));
        }
        return ColorToolbox.ColorIntensify(colorDiffuse, intensity);
    }

}
