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

        public Func<double, double> DistanceInverseLaw { get; set; }

        public int AmtOfRecoursions { get; set; }

        public int ShadowRays { get; set; }

        public Settings()
        {
            BackgroundColor = new Color();
            DistanceInverseLaw = i => Math.Max(Math.Pow(i / 30, 1.1), 1);
            AmtOfRecoursions = 0;
            ShadowRays = 5;
        }
    }
}
