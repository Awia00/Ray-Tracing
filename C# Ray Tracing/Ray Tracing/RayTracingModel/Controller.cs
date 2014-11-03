﻿using System;
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
            _scene.BackgroundColor = Color.FromArgb(100, 130, 155);
            _scene.AmtOfRecoursions = 3;

            // Camera
            _scene.Camera = new SimpleCamera(new Vector3D(2, 2, 2), new Vector3D(0, 0, 0), 16, 10, 1280, 800);

            // Lights
            _scene.SceneLights.Add(new AmbientLight(0.1,Color.FromArgb(200,200,250)));
            _scene.SceneLights.Add(new DirectionalLight(new Vector3D(0.5,1,-0.5), 1, Color.FromArgb(200, 200, 250)));
            //_scene.SceneLights.Add(new DirectionalLight(new Vector3D(0.5, 2, -0.5), 0.5, Color.FromArgb(200, 200, 250)));

            //Objects
            _scene.SceneObjects.Add(new SphereObject3D(new SpecularShader(Color.Firebrick, Color.White, 0.3, 0, 10), new Vector3D(-5,15,0),5));
            _scene.SceneObjects.Add(new SphereObject3D(new DiffuseShader(Color.ForestGreen, 0, 0), new Vector3D(0, 25, 5), 10));
            _scene.SceneObjects.Add(new SphereObject3D(new SpecularShader(Color.MediumBlue, Color.White, 0, 0, 5), new Vector3D(5, 12, -8), 5));
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
