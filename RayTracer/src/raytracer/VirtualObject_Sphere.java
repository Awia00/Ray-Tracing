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
    boolean isReflective;
    private double reflectiveComponent;

    public VirtualObject_Sphere(Position3d centerPos, double radius, IShader shader, boolean isReflective, double reflectiveComponent) {
        this.centerPos = centerPos;
        this.radius = radius;
        this.shader = shader;
        this.isReflective = isReflective;
        this.reflectiveComponent = reflectiveComponent;
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
		
		double a;
		double b;
		double c;
        //calculate a, b, c
        a = Math.pow((vX-pX),2)+(Math.pow((vY-pY),2))+(Math.pow((vZ-pZ),2));
        b = 2*((vX-pX)*(pX-cX)+(vY-pY)*(pY-cY)+(vZ-pZ)*(pZ-cZ));
        c =  cX*cX + cY*cY + cZ*cZ + pX*pX +pY*pY+ pZ*pZ - (2*(cX*pX+cY*pY+cZ*pZ))-radius*radius;
        
		// second method
		//a = Math.pow((pX-cX),2) + Math.pow((pY-cY),2) + Math.pow((pZ-cZ),2) - radius*radius;//(vX * vX + vY * vY + vZ * vZ);
        //c = Math.pow((pX-vX),2) + Math.pow((pY-vY),2) + Math.pow((pZ-vZ),2);
		//b = Math.pow((vX-cX),2) + Math.pow((vY-cY),2) + Math.pow((vZ-cZ),2) -a-c- radius*radius;
        

		
		// calculate d
        double d = b*b-4*a*c;
        // test d for value
        if (d> 0)
        {
            double y = Math.sqrt(d);
            double t1 = b > 0 ? (-b - y) / (2.0 * a) : (-b+y)/(2*a);
            double t2 = c / (t1 * a);
			//System.out.println(t1 + " " + t2);
            if (t1> t2)
            {
                return t2;
            }
            else
                return t1;
        }
        else if (d==0)
        {
            return (-b)/(2*a);
        }
        //calculate t
        // find lowest value of t 
        else return 0.0;
    }

    @Override
    public Vector3d getNormalOnCollisionPosition(Position3d colPos) {
        return new Vector3d(centerPos.getPosX()-colPos.getPosX(), centerPos.getPosY()- colPos.getPosY(), centerPos.getPosZ()-colPos.getPosZ()).normalize();
    }

    @Override
    public IShader getShader() {
        return shader;
    }

    @Override
    public boolean getIsReflective()
    {
        return isReflective;
    }

    @Override
    public double getReflectiveComponent()
    {
        return reflectiveComponent;
    }

}
