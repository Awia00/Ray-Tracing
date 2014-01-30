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
public class Picture {
    private int height, width;
    private int amtPixelHeight, amtPixelWidth;
    // Need an array, where every point has a Color object in it.

    // Need an array, where every point has a Color object in it.
    public Picture(int height, int width, int amtPixelHeight, int amtPixelWidth)
    {
        this.height = height;
        this.width = width;
        this.amtPixelHeight = amtPixelHeight;
        this.amtPixelWidth = amtPixelWidth;
    }

    public int getAmtPixelHeight()
    {
        return amtPixelHeight;
    }

    public int getAmtPixelWidth()
    {
        return amtPixelWidth;
    }

    public int getHeight() {
        return height;
    }

    public int getWidth() {
        return width;
    }
    
    
}
