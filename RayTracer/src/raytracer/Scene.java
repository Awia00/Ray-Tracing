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
    private ArrayList<ILightObject> lights;
    private GUIController gui;

    public Scene(ICamera camera) {
        this.camera = camera;
        virtualObjects = new ArrayList<>();
        lights = new ArrayList<>();
        picture = new Picture(camera.getAmtPixelHeight(), camera.getAmtPixelWidth());
        gui = new GUIController();
        System.out.println("camera width " + camera.getAmtPixelWidth() + " camera height " + camera.getAmtPixelHeight());
        System.out.println("picture width " + picture.getAmtPixelWidth() + " picture height " + picture.getAmtPixelHeight());

    }

    public void initializer() {

    }

    public void setLevelOfRays(int levelOfRays) {
        this.levelOfRays = levelOfRays;
    }

    public void setBackgroundColor(Color backgroundColor) {
        this.backgroundColor = backgroundColor;
    }

    /**
     * create the rays one by one.
     */
    public void createRays() {
        for (int pX = 0; pX < camera.getAmtPixelWidth(); pX++) {
            for (int pY = 0; pY < camera.getAmtPixelHeight(); pY++) {
                Ray ray = camera.createRay(pX, pY);
                picture.setColor(pX, pY, rayTracing(ray, levelOfRays));
                //picture.setColor(pX, pY, shading(ray));
            }
        }
        gui.renderImage(picture);
    }

    /**
     * Takes a ray and checks if it hits any objects, if so create a collision
     * object, and make the shader return the wanted color.
     *
     * @param ray the ray which we follow through the scene
     * @param levelOfRays the amount of times the ray tracing will repeat itself
     * via reflections and refractions.
     * @return the color this ray has got.
     */
    private Color rayTracing(Ray ray, int levelOfRays) {
        IVirtualObject collisionObject = intersection(ray);
        if (collisionObject == null) {
            return backgroundColor;
        }
        Collision collision = getCollision(ray, collisionObject);

        ArrayList<ILightObject> lightsHittingObject = new ArrayList<>();

        for (ILightObject light : lights) {
            Ray rayLight = new Ray(collision.getPosition(), light.getDirectionVector());
            double t = 0;
            for (IVirtualObject virtualObject : virtualObjects) {
                double t2 = virtualObject.checkCollision(rayLight);
                if (t2 < t) {
                    t = t2;
                }
            }
            if (t < -0.01) {
                lightsHittingObject.add(light);
            }
        }

        Color colorOnThisLevel = collisionObject.getShader().getShadingColor(collision, lightsHittingObject);

        // insert recoursive rayTracing method for reflection and refraction;
        // blend the colors
        return colorOnThisLevel;
    }

    public IVirtualObject intersection(Ray ray) {
        double closestCollision = 0;
        IVirtualObject collisionObject = null;
        for (IVirtualObject virtualObject : virtualObjects) {
            double collision;
            collision = virtualObject.checkCollision(ray);
            if (collision > 0 && closestCollision == 0) {
                closestCollision = collision;
                collisionObject = virtualObject;
            }
            if (collision > 0 && collision < closestCollision) {
                closestCollision = collision;
                collisionObject = virtualObject;
            }
        }
        return collisionObject;
    }

    private Collision getCollision(Ray ray, IVirtualObject virtualObject) {
        //Position3d position = virtualObject.getCollisionPosition(ray);
        Position3d position = ray.getCollisionPosition(virtualObject.checkCollision(ray));
        Vector3d normal = virtualObject.getNormalOnCollisionPosition(position);
        Collision collision = new Collision(position, normal);

        return collision;
    }

    public Color shading(Ray ray) {
        if (ray.getVector().getZ() < 0.0) {
            return Color.black;
        } else if (ray.getVector().getX() > 0) {
            return Color.blue;
        } else {
            return Color.red;
        }

    }

    public void addVirtualObject(IVirtualObject virtualObject) {
        virtualObjects.add(virtualObject);
    }

    public void addLightObject(ILightObject lightObject) {
        lights.add(lightObject);
    }
}
