using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingModel
{
    public class Scene
    {
        private static Scene instance = null;
        private bool test = true;
        private Scene()
        {
            
        }

        public Color[,] render()
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

        public static Scene getInstance()
        {
            if(instance == null) instance = new Scene();
            return instance;
        }
    }
}
