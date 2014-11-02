using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using RayTracingModel.Model.Cameras;
using RayTracingModel.Model.Lights;
using RayTracingModel.Model.Objects3D;

namespace RayTracingModel.Model
{
    internal class Scene
    {
        private bool test = true;

        public ICamera Camera { get; set; }
        public IList<IObject3D> SceneObjects { get; private set; }
        public IList<ILight> SceneLights { get; private set; }
        public Color BackgroundColor { get; set; }

        public Scene(ICamera camera)
        {
            Camera = camera;
            SceneObjects = new List<IObject3D>();
            SceneLights = new List<ILight>();
        }

        public Color[,] Render()
        {
            var cameraRays = Camera.GenerateCameraVectors();
            Color[,] colorArray = new Color[cameraRays.GetLength(0), cameraRays.GetLength(1)];

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
            else
            {
                
                for (int i = 0; i < colorArray.GetLength(0); i++)
                {
                    for (int j = 0; j < colorArray.GetLength(1); j++)
                    {
                        var currentRay = cameraRays[i, j];
                        foreach (var sceneObject in SceneObjects)
                        {
                            double distanceToObject = sceneObject.CalculateCollisionPosition(currentRay);
                            if (distanceToObject > 0)
                            {
                                colorArray[i, j] = sceneObject.CalculateColor(SceneLights,
                                currentRay.GetPositionAlongLine(distanceToObject));
                                goto NextPixel;
                            }
                        }
                        colorArray[i, j] = BackgroundColor;
                    NextPixel:
                        ;
                    }
                }
            }
            test = !test;
            return colorArray;
        }
    }
}