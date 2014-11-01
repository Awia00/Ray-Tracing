using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracingModel.Model;

namespace RayTracingModel
{
    public class Controller
    {
        private static Controller _instance = null;
        private Scene _scene;

        private Controller()
        {
            _scene = new Scene(null);
        }

        public static Controller GetInstance()
        {
            if (_instance == null) _instance = new Controller();
            return _instance;
        }

        public Color[,] Render()
        {
            return _scene.Render();
        }
    }
}
