using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracingModel.Model.Lights;

namespace RayTracingModel.Model.Shaders
{
    /// <summary>
    /// <para>Invariants:</para>
    /// <para>@inv (IsReflective() == false && Reflectivity == 0) || (IsReflective() == true && Reflectivity > 0) </para>
    /// <para>@inv (IsRefractive() == false && Refractivity == 0) || (IsRefractive() == true && Refractivity > 0) </para>
    /// </summary>
    public interface IShader
    {
        double Reflectivity { get; set; }
        double Refractivity { get; set; }
        bool IsReflective();
        bool IsRefractive();
        Color CalculateColor(IList<ILight> lightsThatHitsSurface, Vector3D normalVector3D, Vector3D rayVector3D, Vector3D collisionPositionVector3D, double distance);
    }
}
