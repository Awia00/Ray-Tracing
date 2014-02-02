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
public class SimpleOptCamera implements ICamera {

    private double focalDistance;
    private int height, width;
    private int amtPixelHeight, amtPixelWidth;

    /**
     * A Camera with an eye located at coordinate (0,0,0). The picture plane is
     * located perpendicular to the y axis.
     */
    public SimpleOptCamera(double focalDistance, int width, int height, int amtPixelWidth, int amtPixelHeight) {
        this.focalDistance = focalDistance;
        this.height = height;
        this.width = width;
        this.amtPixelHeight = amtPixelHeight;
        this.amtPixelWidth = amtPixelWidth;

    }

    @Override
    public Ray createRay(int pX, int pY) {
        // create the vector
        Vector3d vector = new Vector3d(
                ((-width)/ 2) + pX * (width / amtPixelWidth),
                focalDistance,
                (height / 2) + pY * (-(height / amtPixelHeight)));
        //normalize it
        //vector = Vector3d.normalize(vector);
        return new Ray(new Position3d(0, 0, 0), vector);

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
