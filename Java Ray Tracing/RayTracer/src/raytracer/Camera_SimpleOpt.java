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
public class Camera_SimpleOpt implements ICamera {

    private double focalDistance;
    private double height, width;
    private int amtPixelHeight, amtPixelWidth;

    /**
     * A Camera with an eye located at coordinate (0,0,0). The picture plane is
     * located perpendicular to the y axis.
     */
    public Camera_SimpleOpt(double focalDistance, double width, double height, int amtPixelWidth, int amtPixelHeight) {
        this.focalDistance = focalDistance;
        this.height = height;
        this.width = width;
        this.amtPixelHeight = amtPixelHeight;
        this.amtPixelWidth = amtPixelWidth;

    }

    @Override
    public Ray createRay(int pX, int pY) {
        // create the vector
        Position3d pos = new Position3d(0, 0, 0);
        Vector3d vector = new Vector3d(
                (-(width)/ 2) + pX * (width / (double)amtPixelWidth)-pos.getPosX(),
                focalDistance-pos.getPosY(),
                (-height / 2) + pY * (height / (double)amtPixelHeight)-pos.getPosZ());
        return new Ray(pos, vector);

    }

    @Override
    public int getAmtPixelHeight() {
        return amtPixelHeight;
    }

    @Override
    public int getAmtPixelWidth() {
        return amtPixelWidth;
    }

}
