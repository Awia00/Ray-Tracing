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
        Scene scene = new Scene(new SimpleOptCamera(20, 20, 20, 800, 600));
        scene.setBackgroundColor(Color.darkGray);
        scene.addVirtualObject(new VirtualObjectPlane(new Position3d(5,5,5), new Vector3d(2,2,2), new ShaderDiffuse(Color.orange)));
        scene.createRays();
    }
    
}
