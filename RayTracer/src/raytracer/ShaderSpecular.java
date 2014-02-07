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
public class ShaderSpecular implements IShader {

    private Color colorDiffuse;
    private Color colorSpecular;
    private double specularComponent;

    public ShaderSpecular(Color colorDiffuse, Color colorSpecular, double specularComponent) {
        this.colorDiffuse = colorDiffuse;
        this.colorSpecular = colorSpecular;
        this.specularComponent = specularComponent;
    }

    @Override
    public Color getShadingColor(Collision collision, ArrayList<ILightObject> lights, double ambientCofficient) {
        Color colorToReturn = colorDiffuse;
        if (!lights.isEmpty()) {
            colorToReturn = getDiffuseShading(lights, collision.getNormal(), ambientCofficient);
            // calculate the intensity coming from each light.
            colorToReturn = ColorToolbox.ColorSum(colorToReturn, getSpecularShading(lights, collision));
            return colorToReturn;
        }
        return new Color(0,0,0);
    }

    private Color getDiffuseShading(ArrayList<ILightObject> lights, Vector3d normvect, double ambientCofficient) {
        double intensity = 0;
        for (ILightObject light : lights) {
            intensity += ambientCofficient + light.getIntensity() * (Math.max(0, Vector3d.dotProdukt(normvect, light.getDirectionVector())));
        }
        return ColorToolbox.ColorIntensify(colorDiffuse, intensity);
    }
    
    private Color getSpecularShading(ArrayList<ILightObject> lights, Collision collision)
    {
        double intensity = 0;
        Vector3d negDirectionVector = collision.getIncomingVector().getNegativeVector();
        for (ILightObject light : lights) {
            Vector3d hVector = (Vector3d.sumVector(negDirectionVector.normalize(), light.getDirectionVector()).normalize());
            intensity += light.getIntensity() * Math.pow(Math.max(0,Vector3d.dotProdukt(hVector.normalize(), collision.getNormal().normalize())),specularComponent);
        }
        return ColorToolbox.ColorIntensify(colorSpecular, Math.min(intensity,1));
    }
}
