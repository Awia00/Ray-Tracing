/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package raytracer.GUI;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Graphics;
import javax.swing.JComponent;
import raytracer.Picture;

/**
 *
 * @author Anders
 */
public class RenderComponent extends JComponent {

    private Picture picture;

    public RenderComponent(Picture picture)
    {
        this.picture = picture;
    }

    @Override
    public void paint(Graphics g)
    {
        for (int i = 0 ; i < picture.getAmtPixelWidth() ; i++)
        {
            for (int j = 0 ; j < picture.getAmtPixelHeight() ; j++)
            {
                g.setColor(picture.getColor(i, j));
                g.fillRect(i, j, 1, 1);
            }
        }
    }

    @Override
    public Dimension getPreferredSize()
    {
        return new Dimension(picture.getAmtPixelWidth(), picture.getAmtPixelHeight());
    }

    @Override
    public Dimension getMinimumSize()
    {
        return getPreferredSize();
    }
}
