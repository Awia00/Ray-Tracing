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
public class Position3d {
    private double posX, posY, posZ;

    public Position3d(double posX, double posY, double posZ) {
        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;
    }

    public double getPosX() {
        return posX;
    }

    public double getPosY() {
        return posY;
    }

    public double getPosZ() {
        return posZ;
    }
    
	@Override
	public String toString()
	{
		return "Position3d Object:\n	Position x: " + posX +  ", Position y: " + posY + ", Position z: " + posZ;
	}
    Position3d getPositionPushedOnVector(Vector3d vect)
    {
        return new Position3d(posX+vect.getX(),posY+vect.getY(),posZ+vect.getZ());
    }
}
