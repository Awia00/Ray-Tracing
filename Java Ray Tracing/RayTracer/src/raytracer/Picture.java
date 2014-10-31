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
public class Picture {
    private int amtPixelHeight, amtPixelWidth;
    private Color[][] imageColors;
    // Need an array, where every point has a Color object in it.

    // Need an array, where every point has a Color object in it.
    public Picture(int amtPixelHeight, int amtPixelWidth)
    {
        this.amtPixelHeight = amtPixelHeight;
        this.amtPixelWidth = amtPixelWidth;
        imageColors = new Color[amtPixelWidth][amtPixelHeight];
    }

    public int getAmtPixelHeight()
    {
        return amtPixelHeight;
    }

    public int getAmtPixelWidth()
    {
        return amtPixelWidth;
    }
    
    public void setColor(int pX, int pY, Color color)
    {
        imageColors[pX][pY] = color;
    }
    
    public Color getColor(int pX, int pY)
    {
        return imageColors[pX][pY];
    }
}
