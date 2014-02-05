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
public class VirtualObject_Sphere implements IVirtualObject {

    private Position3d centerPos;
    private double radius;
    private IShader shader;

    public VirtualObject_Sphere(Position3d centerPos, double radius, IShader shader) {
        this.centerPos = centerPos;
        this.radius = radius;
        this.shader = shader;
    }

    @Override
    public double checkCollision(Ray ray) {

        Vector3d rayVector = ray.getVector();
        Position3d rayDot = ray.getPosition();
        // the Vector coordinats
        double vX = rayVector.getX();
        double vY = rayVector.getY();
        double vZ = rayVector.getZ();

        // a Position on the Ray
        double pX = rayDot.getPosX();
        double pY = rayDot.getPosY();
        double pZ = rayDot.getPosZ();
        
        double cX = centerPos.getPosX();
        double cY = centerPos.getPosY();
        double cZ = centerPos.getPosZ();

        //calculate a, b, c
        double a = Math.pow((vX-pX),2)+(Math.pow((vY-pY),2))+(Math.pow((vZ-pZ),2));//(vX * vX + vY * vY + vZ * vZ);
        double b = 2*((vX-pX)*(pX-cX)+(vY-pY)*(pY-cY)+(vZ-pZ)*(pZ-cZ));//2.0 * (pX * vX + pY * vY + pZ * vZ - vX * centerPos.getPosX() - vY * centerPos.getPosY() - vZ * centerPos.getPosZ());;
        double c =  pX * pX - 2 * pX * centerPos.getPosX() + centerPos.getPosX() * centerPos.getPosX() + pY * pY - 2 * pY * centerPos.getPosY() + centerPos.getPosY() * centerPos.getPosY() + pZ * pZ - 2 * pZ * centerPos.getPosZ() + centerPos.getPosZ() * centerPos.getPosZ() - radius * radius;;
        // calculate d
        double d = b*b-4*a*c;
        // test d for value
        if (d>= 0)
        {
            double t1 = (-b - Math.sqrt(d)) / (2.0 * a);
            double t2 = (-b + Math.sqrt(d)) / (2.0 * a);
            if (t1> t2)
            {
                return t2;
            }
            else
                return t1;
        }
        //calculate t
        // find lowest value of t 
        else return 0.0;
    }

    @Override
    public Vector3d getNormalOnCollisionPosition(Position3d colPos) {
        return new Vector3d(colPos.getPosX() - centerPos.getPosX(), colPos.getPosY() - centerPos.getPosY(), colPos.getPosZ() - centerPos.getPosZ());
    }

    @Override
    public IShader getShader() {
        return shader;
    }

}
