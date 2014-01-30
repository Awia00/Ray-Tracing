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
    private Picture picture;

    /**
     * A Camera with an eye located at coord (0,0,0). The picture plane is
     * located perpendicular to the y axis.
     */
    public SimpleOptCamera(double focalDistance, Picture picture)
    {
        this.focalDistance = focalDistance;
        this.picture = picture;
    }

    @Override
    public Ray createRay(int pX, int pY)
    {
        int width = picture.getWidth();
        int height = picture.getHeight();
        int amtPixelWidth = picture.getAmtPixelWidth();
        int amtPixelHeight = picture.getAmtPixelHeight();
        // create the vector
        Vector3d vector = new Vector3d(
                        (-width)/2+pX*(width/amtPixelWidth), 
                        (int) focalDistance, 
                        (height/2)+pY*(-(height/amtPixelHeight)));
        //normalize it
        vector = Vector3d.normalize(vector);
        return new Ray(0, 0, 0,vector);
                
    }

}
