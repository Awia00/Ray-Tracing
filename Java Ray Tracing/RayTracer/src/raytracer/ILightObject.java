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
public interface ILightObject {
    
    Vector3d getDirectionVector();
    
    double getIntensity();
    
    void setIntensity(double intensity);
}
