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
public class VirtualObject_Plane implements IVirtualObject{

    private Position3d planePosition;
    private Vector3d normVector;
    private IShader shading;
    private boolean isReflective;
    private double reflectiveComponent;

    public VirtualObject_Plane(Position3d planePosition, Vector3d normVector, IShader shading, boolean isReflective, double reflectiveComponent) {
        this.planePosition = planePosition;
        this.normVector = normVector.normalize();
        this.shading = shading;
        this.isReflective = isReflective;
        this.reflectiveComponent = reflectiveComponent;
    }
    
    @Override
    public double checkCollision(Ray ray) {
        
        double t = (Vector3d.dotProdukt(normVector, new Vector3d(planePosition.getPosX(),planePosition.getPosY(),planePosition.getPosZ()).normalize()))/(Vector3d.dotProdukt(normVector, ray.getVector()));
        if(t == 0)
        {
            return 0.0;
        }
        else
            return t;
    }

    @Override
    public Vector3d getNormalOnCollisionPosition(Position3d colPos) {
        return normVector;
    }

    @Override
    public IShader getShader() {
        return shading;
    }

    @Override
    public boolean getIsReflective()
    {
        return isReflective;
    }

    @Override
    public double getReflectiveComponent()
    {
        return reflectiveComponent;
    }
    
}
