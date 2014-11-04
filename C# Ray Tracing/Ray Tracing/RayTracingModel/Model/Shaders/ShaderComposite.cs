using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracingModel.Model.Lights;

namespace RayTracingModel.Model.Shaders
{
    public class ShaderComposite : IShader, IEnumerable<IShader>
    {
        public double Reflectivity { get; set; }
        public double Refractivity { get; set; }
        public bool IsReflective()
        {
            throw new NotImplementedException();
        }

        public bool IsRefractive()
        {
            throw new NotImplementedException();
        }

        public Color CalculateColor(IList<ILight> lightsThatHitsSurface, Vector3D normalVector3D, Vector3D rayVector3D, Vector3D collisionPositionVector3D)
        {
            //todo create correct call for adding the different colors together.
            return _children[0].CalculateColor(lightsThatHitsSurface, normalVector3D, rayVector3D, collisionPositionVector3D);
        }

        #region Composite pattern methods
        private List<IShader> _children = new List<IShader>();

        public void AddChild(IShader child)
        {
            _children.Add(child);
        }

        public void RemoveChild(IShader child)
        {
            _children.Remove(child);
        }

        public IShader GetChild(int index)
        {
            return _children[index];
        }

        public IEnumerator<IShader> GetEnumerator()
        {
            foreach (IShader child in _children)
                yield return child;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
