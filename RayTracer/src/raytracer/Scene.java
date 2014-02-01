/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package raytracer;

import java.awt.Color;
import java.util.ArrayList;
import raytracer.GUI.GUIController;

/**
 *
 * @author Anders
 */
public class Scene {

    private ICamera camera;
    private Picture picture;
    private int levelOfRays = 1;
    private Color backgroundColor;
    private ArrayList<IVirtualObject> virtualObjects;

    public Scene(ICamera camera)
    {
        this.camera = camera;
        virtualObjects = new ArrayList<>();
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

    public void setLevelOfRays(int levelOfRays)
    {
        this.levelOfRays = levelOfRays;
    }

    public void setBackgroundColor(Color backgroundColor)
    {
        this.backgroundColor = backgroundColor;
    }
     
    /**
     * create the rays one by one.
     */
    public void createRays()
    {
        for (int pX = 0; pX < camera.getAmtPixelWidth(); pX++)
        {
            for (int pY = 0; pY < camera.getAmtPixelHeight(); pY++)
            {
                Ray ray = camera.createRay(pX, pY);
                rayTracing(ray, levelOfRays);
                picture.setColor(pX, pY, shading(ray));
            }
        }
    }
    
    private void rayTracing(Ray ray, int levelOfRays)
    {
        
    }
    
    public IVirtualObject intersection(Ray ray)
    {
        double closestCollision = 0;
        IVirtualObject collisionObject = null;
        for(IVirtualObject virtualObject : virtualObjects)
        {
            double collision;
            collision = virtualObject.checkCollision(ray);
            if (collision != 0 && closestCollision == 0)
            {
                closestCollision = collision;
                collisionObject = virtualObject;                
            }
            if (collision != 0 && collision < closestCollision)
            {
                closestCollision = collision;
                collisionObject = virtualObject;  
            }
        }
        return collisionObject;
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
