/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package raytracer;

import java.awt.Color;
import raytracer.GUI.GUIController;

/**
 *
 * @author Anders
 */
public class Scene {

    private ICamera camera;
    private Picture picture;

    public Scene(ICamera camera)
    {
        this.camera = camera;
        picture = new Picture(camera.getAmtPixelHeight(), camera.getAmtPixelWidth());
        createRays();
        GUIController gui = new GUIController();
        System.out.println("camera width " + camera.getAmtPixelWidth() + " camera height " + camera.getAmtPixelHeight());
        System.out.println("picture width " + picture.getAmtPixelWidth() + " picture height " + picture.getAmtPixelHeight());
        gui.renderImage(picture);
    }

    public void initializer()
    {

    }

    public void createRays()
    {
        for (int pX = 0; pX < camera.getAmtPixelWidth(); pX++)
        {
            for (int pY = 0; pY < camera.getAmtPixelHeight(); pY++)
            {
                Ray ray = camera.createRay(pX, pY);
                picture.setColor(pX, pY, shading(ray));
            }
        }
    }
    
    public void intersection(Ray ray)
    {
        
    }
    
    public Color shading(Ray ray)
    {
        if (ray.getVector().getZ()< 0.0)
            return Color.black;
        else if (ray.getVector().getX()> -0.9)
            return Color.blue;
        else
            return Color.red;
        
    }

    public static void main(String[] args)
    {
        new Scene(new SimpleOptCamera(20, 200, 200, 200, 200));
    }
}
