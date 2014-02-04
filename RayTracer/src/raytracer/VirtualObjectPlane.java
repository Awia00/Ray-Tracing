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
public class VirtualObjectPlane implements IVirtualObject{

    private Position3d planePosition;
    private Vector3d normVector;
    private IShader shading;

    public VirtualObjectPlane(Position3d planePosition, Vector3d normVector, IShader shading) {
        this.planePosition = planePosition;
        this.normVector = normVector;
        this.shading = shading;
    }
    
    @Override
    public double checkCollision(Ray ray) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public Vector3d getNormalOnCollisionPosition(Position3d colPos) {
        return normVector;
    }

    @Override
    public IShader getShader() {
        return shading;
    }
    
}
