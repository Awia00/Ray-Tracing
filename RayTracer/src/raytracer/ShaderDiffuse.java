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

    Color colorDiffuse;
    Color ambientColor = new Color(20,20,20);

    public ShaderDiffuse(Color colorDiffuse) {
        this.colorDiffuse = colorDiffuse;
    }

    @Override
    public Color getShadingColor(Collision collision, ArrayList<ILightObject> lights) {
        Color colorToReturn = colorDiffuse;
        if (lights.isEmpty()) {
            return ambientColor;
        } else {
            double intensity = 0;
            for (ILightObject light : lights) {
                intensity += light.getIntensity()*(Math.max(0, Vector3d.dotProdukt(collision.getNormal(),light.getDirectionVector())));
            }
            int red = (int)Math.min(255,(intensity)*colorToReturn.getRed());
            int green = (int)Math.min(255,(intensity)*colorToReturn.getGreen());
            int blue = (int)Math.min(255,(intensity)*colorToReturn.getBlue());
            colorToReturn = new Color(Math.max(red, ambientColor.getRed()),Math.max(green, ambientColor.getGreen()),Math.max(blue, ambientColor.getBlue()));
            return colorToReturn;
        }
        
    }

}
