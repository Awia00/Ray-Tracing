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
public class VirtualObjectSphere implements IVirtualObject{

    private Position3d centerPos;
    private double radius;
    private IShader shader;

    public VirtualObjectSphere(Position3d centerPos, double radius, IShader shader) {
        this.centerPos = centerPos;
        this.radius = radius;
        this.shader = shader;
    }
    
    @Override
    public double checkCollision(Ray ray) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
    

    @Override
    public Vector3d getNormalOnCollisionPosition(Position3d colPos) {
        return new Vector3d(colPos.getPosX()-centerPos.getPosX(),colPos.getPosY()-centerPos.getPosY(),colPos.getPosZ()-centerPos.getPosZ());
    }

    @Override
    public IShader getShader() {
        return shader;
    }
    
}
