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
public class Scene {

    private ICamera camera;
    private Picture picture;

    public Scene(ICamera camera, Picture picture)
    {
        this.camera = camera;
        this.picture = picture;
        createRays();
    }

    public void initializer()
    {

    }

    public void createRays()
    {
        for (int pX = 0; pX < picture.getAmtPixelHeight(); pX++)
        {
            for (int pY = 0; pY < picture.getAmtPixelWidth(); pY++)
            {
                Ray ray = camera.createRay(pX, pY);
                ray.print();
            }
        }
    }

    public static void main(String[] args)
    {
        new Scene(new SimpleOptCamera(10,new Picture(200,200,5,5)),new Picture(200,200,5,5) );
    }
}
