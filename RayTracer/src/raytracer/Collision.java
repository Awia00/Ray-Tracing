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
    private Vector3d incomingVector;

    public Collision(Position3d position, Vector3d normal, Vector3d incomingVector) {
        
        this.position = position;
        this.normal = normal.normalize();
        this.incomingVector = incomingVector;
    }

    public Position3d getPosition() {
        return position;
    }

    public Vector3d getNormal() {
        return normal;
    }
    
    public Vector3d getIncomingVector()
    {
        return incomingVector;
    }
    
}
