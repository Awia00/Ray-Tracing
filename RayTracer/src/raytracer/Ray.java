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
    private int x, y, z;
    private Vector3d vector;

    public Ray(int x, int y, int z, Vector3d vector)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.vector = vector;
    }

    public Vector3d getVector()
    {
        return vector;
    }
    
    public void print()
    {
        System.out.println("Ray: dotX=" + x + " dotY=" + y + " dotZ=" + z);
        System.out.println("Vector: vectX=" + vector.getX() + " vectY=" + vector.getY() + " vectZ=" + vector.getZ());
        System.out.println("Vector length = " + Vector3d.getVectorLenght(vector));
        System.out.println("");
    }
    
    
}
