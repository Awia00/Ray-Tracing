using System;
using System.Collections.Generic;
using System.Drawing;

namespace RayTracingModel.Model
{
    public enum PostEffect { DepthOfField, DepthMap }
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

        public IList<PostEffect> PostEffects { get; set; }

        public double FocalNear { get; set; }
        public double FocalFar { get; set; }

        public Settings()
        {
            BackgroundColor = new Color();
            PostEffects = new List<PostEffect>();
            DistanceInverseLawCamera = i => 1/Math.Max(Math.Pow(i / 3000, 1.2), 1);
            DistanceInverseLawLight = i => Math.Max(Math.Pow(i/50 , 2), 1);
            AmtOfRecoursions = 0;
            ShadowRays = 5;
            SoftShadowSpread = 0.1;
            SpaceRefractionIndex = 1.0;
        }
    }
}
