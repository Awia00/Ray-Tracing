using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Color_Toolbox;
using RayTracingModel.Model.Cameras;
using RayTracingModel.Model.Lights;
using RayTracingModel.Model.Objects3D;

namespace RayTracingModel.Model
{
    internal class Scene
    {
        private bool test = true;

        public int AmtOfRecoursions { get; set; }
        private int _currentRecoursion = 0;
        private IObject3D _currentObject3D = null;

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
                        colorArray[i, j] = Color.FromArgb(Math.Min(0 + j, 255),
                            Math.Max(255 - i, 0), Math.Max(255 - j, 0));
                        Color.FromArgb(0, 255, 255); // j = 0, i = 0 bottom left
                        Color.FromArgb(0, 0, 255); // i = 255, j= 0 bottom right
                        Color.FromArgb(255, 255, 0); // i = 0, j= 255 top left
                        Color.FromArgb(255, 0, 0); // i = 255, j= 255 top right
                    }
                }
            }
            else
            {
                for (int i = 0; i < colorArray.GetLength(0); i++)
                {
                    for (int j = 0; j < colorArray.GetLength(1); j++)
                    {
                        colorArray[i, j] = CalculateObjectCollisions(cameraRays[i, j]);
                        _currentRecoursion = 0;
                        _currentObject3D = null;
                    }
                }
            }
            test = !test;
            return colorArray;
        }

        private Color CalculateObjectCollisions(Line3D ray)
        {
            IObject3D closestObject = null;
            double closestObjectDistance = double.MaxValue;
            foreach (var sceneObject in SceneObjects)
            {
                if (_currentObject3D != sceneObject)
                {
                    double distanceToObject = sceneObject.CalculateCollisionPosition(ray);
                    if (distanceToObject > 0 && distanceToObject < closestObjectDistance)
                    {
                        closestObject = sceneObject;
                        closestObjectDistance = distanceToObject;
                    }
                }
            }
            if (closestObject == null) return BackgroundColor;
            return ComputeColor(ray, closestObject, ray.GetPositionAlongLine(closestObjectDistance));
        }

        private Color ComputeColor(Line3D ray, IObject3D collisionObject, Vector3D collisionPosition)
        {
            _currentRecoursion++;
            Color baseColor = collisionObject.CalculateColor(LightsNotInShadow(collisionPosition), collisionPosition,
                ray.DirectionVector);
            Color reflectionColor = new Color();
            if (_currentRecoursion <= AmtOfRecoursions)
            {
                if (collisionObject.Shader.IsReflective())
                {
                    _currentObject3D = collisionObject;
                    //generate the reflection ray and run CalculateObject Collisions again.

                    Line3D reflectRay = new Line3D(collisionPosition, Vector3D.ReflectionVector(ray.DirectionVector,collisionObject.CalculateNormVector(collisionPosition)));
                    reflectionColor = CalculateObjectCollisions(reflectRay);
                    return ColorToolbox.BlendSimpleByAmt(reflectionColor, baseColor, collisionObject.Shader.Reflectivity);

                }
                if (collisionObject.Shader.IsRefractive())
                {
                    _currentObject3D = collisionObject;
                    //generate the refraction ray and run CalculateObject Collisions again.
                }
            }
            _currentRecoursion--;
            return baseColor; // 
        }

        private IList<ILight> LightsNotInShadow(Vector3D positionOnObject)
        {
            var tempList = new List<ILight>();
            foreach (var light in SceneLights)
            {
                // todo create fix such that the light can be local and between the objects.
                var lightVector = light.CalculateLightDirectionOnPosition(positionOnObject);
                var ray = new Line3D(positionOnObject, lightVector.VectorNegation());

                bool inShadow = false;
                foreach (var sceneObject in SceneObjects)
                {
                    if (sceneObject.CalculateCollisionPosition(ray) > 0) inShadow = true;
                }
                if (!inShadow) {tempList.Add(light);}
            }
            return tempList;
        }
    }
}