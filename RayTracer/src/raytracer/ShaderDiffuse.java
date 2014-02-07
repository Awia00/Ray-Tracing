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
    public Color getShadingColor(Collision collision, ArrayList<ILightObject> lights, double ambientCofficient) {
        Color colorToReturn = colorDiffuse;
        if (!lights.isEmpty()) {
            colorToReturn = getDiffuseShading(lights, collision.getNormal(), ambientCofficient);
            // calculate the intensity coming from each light.
            return colorToReturn;
        }
        return null;
    }

    private Color getDiffuseShading(ArrayList<ILightObject> lights, Vector3d normvect, double ambientCofficient) {
        double intensity = 0;
        for (ILightObject light : lights) {
            intensity += ambientCofficient +light.getIntensity() * (Math.max(0, Vector3d.dotProdukt(normvect, light.getDirectionVector())));
        }
        return ColorToolbox.ColorIntensify(colorDiffuse, intensity);
    }

}
