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
public interface IVirtualObject {

    double checkCollision(Ray ray);

    Position3d getCollisionPosition(Ray ray);

    Vector3d getNormalOnCollisionPosition(Position3d colPos);

    IShader getShader();
}
