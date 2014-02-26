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
    private int levelOfRays = 0;
    private Color backgroundColor;
    private double ambientCofficient;
    private ArrayList<IVirtualObject> virtualObjects;
    private ArrayList<ILightObject> lights;
    private GUIController gui;
    private IVirtualObject lastObject;

    public Scene(ICamera camera) {
        this.camera = camera;
        virtualObjects = new ArrayList<>();
        lights = new ArrayList<>();
        picture = new Picture(camera.getAmtPixelHeight(), camera.getAmtPixelWidth());
        gui = new GUIController();
        backgroundColor = new Color(0, 0, 0);
        ambientCofficient = 0;
        IVirtualObject lastObject = null;
        printSceneInfo();
    }

    public void printSceneInfo() {
        System.out.println("camera pixels width " + camera.getAmtPixelWidth() + " camera pixels height " + camera.getAmtPixelHeight());
        //System.out.println("picture width " + picture.getAmtPixelWidth() + " picture height " + picture.getAmtPixelHeight());
        System.out.println("Backgroundcolor" + backgroundColor);
    }

    public void initializer() {

    }

    public void setLevelOfRays(int levelOfRays) {
        this.levelOfRays = levelOfRays;
    }

    public void setBackgroundColor(Color backgroundColor) {
        this.backgroundColor = backgroundColor;
    }

    public void setAmbientLight(double ambientCofficient) {
        this.ambientCofficient = ambientCofficient;
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
     * @param levelOfRays2 the amount of times the ray tracing will repeat itself
     * via reflections and refractions.
     * @return the color this ray has got.
     */
    private Color rayTracing(Ray ray, int levelOfRays2) {


        IVirtualObject collisionObject = intersection(ray);
        if (collisionObject == null) {
            return backgroundColor;
        }
        if (levelOfRays2 == 0)
        {
            //System.out.println(ray.toString() + "\n");
        }
        lastObject = collisionObject;
        
        Collision collision = getCollision(ray, collisionObject);

        Color colorOnThisLevel = collisionObject.getShader().getShadingColor(collision, lights, ambientCofficient);

        Color reflectiveColor = null;
        Vector3d reflective = null;

        if (collisionObject.getIsReflective() && levelOfRays2 != 0) {
            Vector3d cameraVector = ray.getVector();
            Vector3d norm = collision.getNormal();
            Vector3d ln = norm.getVectorTimesDouble(Vector3d.dotProdukt(norm, cameraVector.getNegativeVector()));
            reflective = Vector3d.sumVector(cameraVector,ln.getVectorTimesDouble(2));
            //reflective = Vector3d.sumVector(cameraVector.getNegativeVector(), (norm.getVectorTimesDouble(2 * Vector3d.dotProdukt(norm, cameraVector))));
            Ray reflectRay = new Ray(collision.getPosition(), reflective);
            reflectiveColor = rayTracing(reflectRay, --levelOfRays2);
            }
        
        if (reflectiveColor != null) {
            colorOnThisLevel = ColorToolbox.ColorBlendPct(colorOnThisLevel, reflectiveColor, collisionObject.getReflectiveComponent());//*Math.pow(Math.max(0,(Vector3d.dotProdukt(ray.getVector(), reflective))),5));
        }
        lastObject = null;
        return colorOnThisLevel;
    }

    
    public IVirtualObject intersection(Ray ray) {
        double closestCollision = 0;
        IVirtualObject collisionObject = null;
        for (IVirtualObject virtualObject : virtualObjects) {
            if (!virtualObject.equals(lastObject)) {
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
        }
        return collisionObject;
    }

    private Collision getCollision(Ray ray, IVirtualObject virtualObject) {
        //Position3d position = virtualObject.getCollisionPosition(ray);
        Position3d position = ray.getCollisionPosition(virtualObject.checkCollision(ray)); // remember to optimise this.
        Vector3d normal = virtualObject.getNormalOnCollisionPosition(position);
        Collision collision = new Collision(position, normal, ray.getVector());

        return collision;
    }

    public void addVirtualObject(IVirtualObject virtualObject) {
        virtualObjects.add(virtualObject);
    }

    public void addLightObject(ILightObject lightObject) {
        lights.add(lightObject);
    }
}
