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
public abstract class ColorToolbox {

    public static Color ColorSum(Color color1, Color color2) {
        int red = Math.min(255, color1.getRed() + color2.getRed());
        int green = Math.min(255, color1.getGreen() + color2.getGreen());
        int blue = Math.min(255, color1.getBlue() + color2.getBlue());
        return new Color(red, green, blue);
    }

    public static Color ColorIntensify(Color color1, double intensity) {
        int red = (int) Math.min(255, color1.getRed() * (intensity));
        int green = (int) Math.min(255, color1.getGreen() * (intensity));
        int blue = (int) Math.min(255, color1.getBlue() * (intensity));
        return new Color(red, green, blue);
    }

    /**
     * Blend to colors with a pct to each, the pct tells the weight of the
     * second color
     *
     * @param color1
     * @param color2
     * @param pct between 0.0 and 1.0
     * @return
     */
    public static Color ColorBlendPct(Color color1, Color color2, double pct) {
        int red = (int) (color1.getRed() * (1.0 - pct) + (color2.getRed() * pct));
        int green = (int) (color1.getGreen() * (1.0 - pct) + (color2.getGreen() * pct));
        int blue = (int) (color1.getBlue() * (1.0 - pct) + (color2.getGreen() * pct));
        return new Color(red, green, blue);
    }
}
