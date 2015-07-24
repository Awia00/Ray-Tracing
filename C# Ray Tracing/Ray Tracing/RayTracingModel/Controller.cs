using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
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
            _scene.Settings.AmtOfRecoursions = 3;
            _scene.Settings.ShadowRays = 128;
            _scene.Settings.PostEffects = new List<PostEffect>{PostEffect.DepthOfField};
            _scene.Settings.FocalNear = 25;
            _scene.Settings.FocalFar = 30;

            // Camera
            //_scene.Camera = new SimpleCamera(new Vector3D(4, 4, 4), new Vector3D(0, -5, 7), 16, 10, 640, 400); // low res 16 10 ratio
            _scene.Camera = new SimpleCamera(new Vector3D(4, 4, 4), new Vector3D(0, -5, 7), 16, 10, 960, 600); // medium res 16 10 ratio
            //_scene.Camera = new SimpleCamera(new Vector3D(4, 4, 4), new Vector3D(0, -5, 7), 16, 10, 1280, 800); // HD 16 10 ratio
            //_scene.Camera = new SimpleCamera(new Vector3D(4, 4, 4), new Vector3D(0, -5, 7), 16, 10, 1920, 1200); // Full HD 16 10 ratio

            //_scene.Camera = new SimpleStereoCamera(new Vector3D(4, 4, 4), new Vector3D(-1, -5, 3), new Vector3D(1, -5, 3), 22, 10, 800, 300);
            //_scene.Camera = new SimpleStereoCamera(new Vector3D(4, 4, 4), new Vector3D(-2, -5, 3), new Vector3D(2, -5, 3), 22, 10, 1600, 600);

            // Lights
            _scene.SceneLights.Add(new AmbientLight(0.1,Color.Wheat));
            _scene.SceneLights.Add(new DirectionalLight(new Vector3D(0.5,-0.1,-0.5), 0.8, Color.Wheat));
            _scene.SceneLights.Add(new DirectionalLight(new Vector3D(-0.1, 1, 0.1), 0.3, Color.Wheat));
            _scene.SceneLights.Add(new LocalLight(1.2, Color.Wheat, new Vector3D(0,8,20)));

            //Objects
            _scene.SceneObjects.Add(new SphereObject3D(new SpecularShader(Color.Silver, Color.White, 10,        0.3, 0, 1.5),    new Vector3D(-10,15,0),5));

            _scene.SceneObjects.Add(new SphereObject3D(new DiffuseShader(Color.ForestGreen,                     0.05, 0, 1.2),    new Vector3D(0, 25, 5), 10));

            _scene.SceneObjects.Add(new SphereObject3D(new SpecularShader(Color.MediumBlue, Color.White, 5,     0, 0, 1.2),    new Vector3D(10, 20, -2), 3));

            _scene.SceneObjects.Add(new SphereObject3D(new SpecularShader(Color.Silver, Color.White, 10,        0.05, 0.7, 1.12),   new Vector3D(-4, 11, 0), 3));

            _scene.SceneObjects.Add(new SphereObject3D(new SpecularShader(Color.DarkRed, Color.White, 10, 0.15, 0, 1.12), new Vector3D(-10, 50, 35), 7));

            _scene.SceneObjects.Add(new SphereObject3D(new SpecularShader(Color.Aqua, Color.White, 10, 0.15, 0, 1.12), new Vector3D(10, 50, 35), 7));

            _scene.SceneObjects.Add(new PlaneObject3D()
            {
                Shader = new DiffuseShader(Color.Goldenrod, 0, 0, 1),
                CenterPositionVector3D = new Vector3D(0, 5, -5),
                NormalVector3D = new Vector3D(0, 0, 1)
            });

            _scene.SceneObjects.Add(new PlaneObject3D()
            {
                Shader = new DiffuseShader(Color.Black, 0, 0, 1),
                CenterPositionVector3D = new Vector3D(0, -5, -5),
                NormalVector3D = new Vector3D(0, -1, 0)
            });

            _scene.SceneObjects.Add(new PlaneObject3D()
            {
                Shader = new DiffuseShader(Color.Wheat, 0, 0, 1),
                CenterPositionVector3D = new Vector3D(0, 80, 0),
                NormalVector3D = new Vector3D(0, -1, 0)
            });

            _scene.SceneObjects.Add(new PlaneObject3D()
            {
                Shader = new DiffuseShader(Color.CornflowerBlue, 0, 0, 1),
                CenterPositionVector3D = new Vector3D(-40, 0, -0),
                NormalVector3D = new Vector3D(1, 0, 0)
            });

            _scene.SceneObjects.Add(new PlaneObject3D()
            {
                Shader = new DiffuseShader(Color.IndianRed, 0, 0, 1),
                CenterPositionVector3D = new Vector3D(40, 0, 0),
                NormalVector3D = new Vector3D(-1, 0, 0)
            });
        }

        public static Controller GetInstance()
        {
            return _instance ?? (_instance = new Controller());
        }

        async public Task<Color[,]> Render()
        {
            //Func<Color[,]> temp = () => _scene.AsyncRender().Result; // Async rendering
            Func<Color[,]> temp = () => _scene.Render(); // sync rendering
            var array = await Task.Run(temp);
            return array;
        }

        async public Task<int> GetRenderProgress()
        {
            await Task.Run(() => Thread.Sleep(100));
            return (int)(Scene.RenderProgress*100);
        }
    }
}
