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
public interface ICamera {
    
    // creates a ray from the camera's eye to the pixel with coordinat (px,py,d)
    Ray createRay(int pX, int pY);
    
}
