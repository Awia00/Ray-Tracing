using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingModel.Model
{
    public class Settings
    {
        public Color BackgroundColor { get; set; }

        public double SpaceRefractionIndex { get; set; }

        public Func<double, double> DistanceInverseLawCamera { get; set; }
        public Func<double, double> DistanceInverseLawLight { get; set; }

        public int AmtOfRecoursions { get; set; }

        public int ShadowRays { get; set; }

        public double SoftShadowSpread { get; set; }

        public int Interpolation { get; set; }

        public Settings()
        {
            BackgroundColor = new Color();
            DistanceInverseLawCamera = i => 1/Math.Max(Math.Pow(i / 100, 2), 1);
            DistanceInverseLawLight = i => Math.Max(Math.Pow(i/50 , 2), 1);
            AmtOfRecoursions = 0;
            ShadowRays = 5;
            SoftShadowSpread = 0.1;
            SpaceRefractionIndex = 1;
        }
    }
}
