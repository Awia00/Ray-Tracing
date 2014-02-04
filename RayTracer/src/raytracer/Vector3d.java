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

    public double getVectorLenght()
    {
        return Math.round(Math.sqrt(Math.pow(x, 2.0)
                + Math.pow(y, 2.0)
                + Math.pow(z, 2.0)));
    }

    public Vector3d normalize()
    {
        double length = getVectorLenght();
        return new Vector3d(x / length,
                            y / length,
                            z / length);
    }
    
    public static double dotProdukt(Vector3d vect1, Vector3d vect2)
    { 
        return vect1.getX()*vect2.getX() + vect1.getY()*vect2.getY()+vect1.getZ()*vect2.getZ();
    }
    
    public static double getCosV(Vector3d vect1, Vector3d vect2)
    {
        return (Vector3d.dotProdukt(vect1, vect2))/(vect1.getVectorLenght()*vect2.getVectorLenght());
    }
}
