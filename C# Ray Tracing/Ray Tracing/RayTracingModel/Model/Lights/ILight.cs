using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingModel.Model.Lights
{
    public interface ILight
    {
        double Intensity { get; set; }
        Color Color { get; set; }
        Vector3D CalculateLightDirectionOnPosition(Vector3D positionVector3D);
    }
}
