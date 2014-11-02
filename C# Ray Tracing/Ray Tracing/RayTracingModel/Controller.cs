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

            // Camera
            _scene.Camera = new SimpleCamera(new Vector3D(1, 1, 1), new Vector3D(0, -5, 0), 10, 16, 800, 1280);

            // Lights
            _scene.SceneLights.Add(new AmbientLight(0.3,Color.FromArgb(200,200,250)));

            //Objects
            _scene.SceneObjects.Add(new SphereObject3D(new FlatShader(Color.Firebrick,0,0),new Vector3D(0,15,0),5));
            _scene.SceneObjects.Add(new SphereObject3D(new FlatShader(Color.ForestGreen, 0, 0), new Vector3D(0, 15, 1), 5));
            
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
