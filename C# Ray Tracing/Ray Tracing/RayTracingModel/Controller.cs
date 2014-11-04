using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracingModel.Model;
using RayTracingModel.Model.Cameras;
using RayTracingModel.Model.Lights;
using RayTracingModel.Model.Objects3D;
using RayTracingModel.Model.Shaders;

namespace RayTracingModel
{
       // todo implement refraction index and refraction vectors.
    // todo implement photon loss based on an objects material AKA an object of olive oil would take up more of the light.
    // todo refactor reflection and refraction into their own classes with interface material. or into the same class "Material"
    // todo make code async from application
    public class Controller
    {
        private static Controller _instance = null;
        private Scene _scene;

        private Controller()
        {
            _scene = new Scene(null);
            TestSettings();
        }

        private void TestSettings()
        {
            // Settings
            _scene.Settings.BackgroundColor = Color.FromArgb(100, 130, 155);
            _scene.Settings.AmtOfRecoursions = 2;
            _scene.Settings.ShadowRays = 12;

            // Camera
            _scene.Camera = new SimpleCamera(new Vector3D(3, 3, 3), new Vector3D(0, 0, 2), 16, 10, 640, 400); // low res 16 10 ratio
            //_scene.Camera = new SimpleCamera(new Vector3D(3, 3, 3), new Vector3D(0, 0, 2), 16, 10, 1280, 800); // HD 16 10 ratio
            //_scene.Camera = new SimpleCamera(new Vector3D(3, 3, 3), new Vector3D(0, 0, 2), 16, 10, 1920, 1200); // Full HD 16 10 ratio

            // Lights
            _scene.SceneLights.Add(new AmbientLight(0.1,Color.FromArgb(200,200,250)));
            _scene.SceneLights.Add(new DirectionalLight(new Vector3D(0.5,-0.1,-0.5), 1, Color.FromArgb(200, 200, 250)));
            _scene.SceneLights.Add(new DirectionalLight(new Vector3D(-0.1, 1, 0.1), 0.5, Color.FromArgb(200, 200, 250)));

            //Objects
            _scene.SceneObjects.Add(new SphereObject3D(new SpecularShader(Color.Silver, Color.White, 0.3, 0, 10), new Vector3D(-10,15,0),5));
            _scene.SceneObjects.Add(new SphereObject3D(new DiffuseShader(Color.ForestGreen, 0, 0), new Vector3D(0, 25, 5), 10));
            _scene.SceneObjects.Add(new SphereObject3D(new SpecularShader(Color.ForestGreen, Color.White, 0, 0, 10), new Vector3D(75, 100, 5), 10));
            _scene.SceneObjects.Add(new SphereObject3D(new SpecularShader(Color.MediumBlue, Color.White, 0, 0, 5), new Vector3D(10, 20, -2), 3));
            _scene.SceneObjects.Add(new PlaneObject3D()
            {
                Shader = new DiffuseShader(Color.Goldenrod,0.5,0),
                CenterPositionVector3D = new Vector3D(0,5,-5),
                NormalVector3D = new Vector3D(0,0,-1).VectorNegation()
            });
        }

        public static Controller GetInstance()
        {
            if (_instance == null) _instance = new Controller();
            return _instance;
        }

        async public Task<Color[,]> Render()
        {
            Func<Color[,]> temp = () => _scene.Render();
            return await Task.Run(temp);
        }
    }
}
