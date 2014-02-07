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
        Scene scene = new Scene(new Camera_SimpleOpt(25, 50, 40, 1400, 1000));
        scene.setBackgroundColor(new Color(10,10,25));
        scene.setAmbientLight(0.1);
        scene.setLevelOfRays(1);
        
        //scene.addVirtualObject(new VirtualObject_Plane(new Position3d(0,0,-1750), new Vector3d(0,0,1).normalize(), new ShaderDiffuse(Color.GREEN), false, 0.5));
        
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(75,200,50),40,new ShaderDiffuse(Color.BLUE),true,0.1));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(20,100,-40),35,new ShaderSpecular(Color.red, Color.white, 100),true,0.1));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(0,400,-50),50,new ShaderDiffuse(Color.orange),true,0.1));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(-25,250,10),10,new ShaderSpecular(Color.CYAN, Color.white, 100),true,0.1));
        //
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(-35,350,10),30,new ShaderSpecular(Color.DARK_GRAY, Color.white, 100),true,0.1));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(2,250,50),11,new ShaderSpecular(Color.MAGENTA, Color.white, 100),true,0.1));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(-12,250,33),12,new ShaderSpecular(Color.PINK, Color.white, 100),true,0.1));
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(-43,250,98),13,new ShaderSpecular(Color.YELLOW, Color.white, 100),true,0.1));
        
        scene.addVirtualObject(new VirtualObject_Sphere(new Position3d(0,50,-140),100,new ShaderSpecular(Color.white, Color.white, 100),true,0.6));
        
        //scene.addLightObject(new LightObject_Directional(new Vector3d(-1,-1,0),1));
        scene.addLightObject(new LightObject_Directional(new Vector3d(-1,1,0),0.9));
        //scene.addLightObject(new LightObject_Directional(new Vector3d(5,-2,-2),0.2));
        scene.createRays();
    }
    
}
