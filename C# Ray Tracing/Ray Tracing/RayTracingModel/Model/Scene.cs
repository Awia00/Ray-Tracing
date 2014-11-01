using System;
using System.Drawing;

namespace RayTracingModel.Model
{
    class Scene
    {
        private bool test = true;
        public Scene()
        {
            
        }

        public Color[,] Render()
        {
            Color[,] colorArray = new Color[300, 400];

            if (test)
            {
                for (int i = 0; i < colorArray.GetLength(0); i++)
                {
                    for (int j = 0; j < colorArray.GetLength(1); j++)
                    {
                        colorArray[i, j] = Color.FromArgb(255, Math.Min(0 + j, 255),
                            Math.Max(255 - i, 0), Math.Max(255 - j, 0));
                    }
                }
                
            }
            test = !test;
            return colorArray;
        }
    }
}
