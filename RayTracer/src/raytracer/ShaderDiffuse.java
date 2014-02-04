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
public class ShaderDiffuse implements IShader {

    Color colorDiffuse;

    public ShaderDiffuse(Color colorDiffuse) {
        this.colorDiffuse = colorDiffuse;
    }

    @Override
    public Color getShadingColor(Collision collision) {
        return colorDiffuse;
    }
    
}
