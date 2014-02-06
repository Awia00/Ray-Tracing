/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package raytracer;

import java.awt.Color;

/**
 * 
 * @author Anders
 */
public class RayTracer {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        Scene scene = new Scene(new Camera_SimpleOpt(25, 50, 40, 800, 600));
        scene.setBackgroundColor(new Color(10,10,25));
        scene.setAmbientLight(new Color(13,13,30));
        scene.setLevelOfRays(3);
        //scene.addVirtualObject(new VirtualObject_Plane(new Position3d(5,500,5), new Vector3d(2,2,2), new ShaderDiffuse(Color.GREEN)));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(75,200,50),40,new ShaderDiffuse(Color.BLUE),true,0.9));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(20,150,-40),35,new ShaderSpecular(Color.red, Color.white, 100),true,0.5));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(0,400,-50),50,new ShaderDiffuse(Color.orange),true,0.5));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(-25,250,10),10,new ShaderSpecular(Color.CYAN, Color.white, 100),true,0.5));
        //
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(-35,350,10),30,new ShaderSpecular(Color.CYAN, Color.white, 100),true,0.5));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(2,250,50),10,new ShaderSpecular(Color.CYAN, Color.white, 100),true,0.5));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(-12,250,33),10,new ShaderSpecular(Color.CYAN, Color.white, 100),true,0.5));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(-43,250,98),10,new ShaderSpecular(Color.CYAN, Color.white, 100),true,0.5));
        
        scene.addLightObject(new LightObject_Directional(new Vector3d(-0.7,-0.7,0).normalize(),1));
        scene.addLightObject(new LightObject_Directional(new Vector3d(-1,1,0),0.9));
        //scene.addLightObject(new LightObject_Directional(new Vector3d(5,-2,-2),0.2));
        scene.createRays();
    }
    
}
