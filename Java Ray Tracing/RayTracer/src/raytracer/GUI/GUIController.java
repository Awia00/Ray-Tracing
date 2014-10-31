/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package raytracer.GUI;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Container;
import java.awt.Dimension;
import java.awt.Graphics;
import javax.swing.JButton;
import javax.swing.JComponent;
import javax.swing.JFrame;
import javax.swing.JPanel;
import raytracer.Picture;

/**
 *
 * @author Anders
 */
public class GUIController {

    private JFrame frame;
    private Container container;
    private RenderComponent render;

    public GUIController()
    {
        frame = new JFrame("Ray Tracer");
        container = new JPanel();
        
        frame.getContentPane().add(container);
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.pack();
        frame.setVisible(true);

    }

    public void renderImage(Picture picture)
    {
        render = new RenderComponent(picture);
        container.add(render);
        frame.repaint();
        frame.pack();
    }
}
