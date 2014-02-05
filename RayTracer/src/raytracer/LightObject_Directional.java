/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package raytracer;

/**
 *
 * @author Anders
 */
public class LightObject_Directional implements ILightObject {
    Vector3d directionVector;
    double intensity;

    public LightObject_Directional(Vector3d directionVector, double intensity) {
        this.directionVector = directionVector.normalize();
        this.intensity = intensity;
    }
    
    @Override
    public Vector3d getDirectionVector()
    {
        return directionVector;
    }

    @Override
    public double getIntensity() {
        return intensity;
    }

    @Override
    public void setIntensity(double intensity) {
        this.intensity = intensity;
    }

    
    
    
}
