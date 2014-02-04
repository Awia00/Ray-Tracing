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
public class Ray {
    private Position3d position;
    private Vector3d vector;

    public Ray(Position3d position, Vector3d vector)
    {
        this.position = position;
        this.vector = vector;
    }

    public Vector3d getVector()
    {
        return vector;
    }

    public Position3d getPosition() {
        return position;
    }
    
    
    
    public void print()
    {
        System.out.println("Vector: vectX=" + vector.getX() + " vectY=" + vector.getY() + " vectZ=" + vector.getZ());
        System.out.println("Vector length = " + vector.getVectorLenght());
        System.out.println("");
    }
    
    public Position3d getCollisionPosition(double t)
    {
        return new Position3d(position.getPosX()+t*vector.getX(), 
                              position.getPosY()+t*vector.getY(),
                              position.getPosZ()+t*vector.getZ());
    }
}
