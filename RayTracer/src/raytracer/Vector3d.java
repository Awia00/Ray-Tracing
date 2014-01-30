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
public class Vector3d {

    private double x, y, z;

    public Vector3d(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public double getX()
    {
        return x;
    }

    public double getY()
    {
        return y;
    }

    public double getZ()
    {
        return z;
    }

    public static double getVectorLenght(Vector3d vector)
    {
        return Math.sqrt(Math.pow(vector.getX(), 2.0)
                + Math.pow(vector.getY(), 2.0)
                + Math.pow(vector.getZ(), 2.0));
    }

    public static Vector3d normalize(Vector3d vector)
    {
        double length = Vector3d.getVectorLenght(vector);
        return new Vector3d(vector.getX() / length,
                            vector.getY() / length,
                            vector.getZ() / length);
    }
}
