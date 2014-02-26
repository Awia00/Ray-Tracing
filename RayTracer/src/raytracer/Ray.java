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
        this.vector = vector.normalize();
    }

    public Vector3d getVector()
    {
        return vector;
    }

    public Position3d getPosition() {
        return position;
    }
    
    
    @Override
	public String toString()
	{
		return "Ray Object:\n	" + position.toString() + "\n	" + vector.toString();
	}
	
    public Position3d getCollisionPosition(double t)
    {
        return new Position3d(position.getPosX()*(t-1)+(t*vector.getX()), 
                              position.getPosY()*(t-1)+(t*vector.getY()),
                              position.getPosZ()*(t-1)+(t*vector.getZ()));
    }
}
