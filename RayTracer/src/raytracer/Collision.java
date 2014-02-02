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
public class Collision {
    
    private Position3d position;
    private Vector3d normal;

    public Collision(Position3d position, Vector3d normal) {
        
        this.position = position;
        this.normal = normal;
    }
    
}
