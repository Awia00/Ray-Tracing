using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Color_Toolbox;
using RayTracingModel.Model.Cameras;
using RayTracingModel.Model.Lights;
using RayTracingModel.Model.Objects3D;

namespace RayTracingModel.Model
{
    internal class Scene
    {
        private bool test = true;

        [ThreadStatic] private static int _currentRecoursion = 0;
        [ThreadStatic] private static double _distanceTravelled = 0;
        public static double RenderProgress { get; set; }

        public ICamera Camera { get; set; }
        public IList<IObject3D> SceneObjects { get; private set; }
        public IList<ILight> SceneLights { get; private set; }
        public Settings Settings { get; set; }

        public Scene(ICamera camera)
        {
            Camera = camera;
            SceneObjects = new List<IObject3D>();
            SceneLights = new List<ILight>();
            Settings = new Settings();
        }

        async public Task<Color[,]> Render()
        {
            RenderProgress = 0;
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
                RenderProgress = 1;
            }
            else
            {
                var tasks = new List<Task<Tuple<int, int, Color>>>();
                for (int i = 0; i < colorArray.GetLength(0); i++)
                {
                    for (int j = 0; j < colorArray.GetLength(1); j++)
                    {
                        tasks.Add(AssignColor(i,j,cameraRays[i,j]));
                        
                    }
                }
                foreach (var task in await Task.WhenAll(tasks))
                {
                    colorArray[task.Item1, task.Item2] = task.Item3;
                }
            }
            test = !test;
            return colorArray;
        }

        async private Task<Tuple<int,int,Color>> AssignColor(int i, int j, Line3D ray)
        {
            Func<Color> tempColorFunc = () => CalculateObjectCollisions(ray);
            var tempColor = await Task.Run(tempColorFunc);
            RenderProgress += 1.0 / (Camera.AmtOfHeightPixels * Camera.AmtOfWidthPixels);
            _currentRecoursion = 0;
            _distanceTravelled = 0;
            return Tuple.Create(i, j, tempColor);
        }

        private Color CalculateObjectCollisions(Line3D ray)
        {
            ray.PushStartPositionAlongLine(0.01);
            IObject3D closestObject = null;
            double closestObjectDistance = double.MaxValue;
            foreach (var sceneObject in SceneObjects)
            {
                double distanceToObject = sceneObject.CalculateCollisionPosition(ray);
                if (distanceToObject > 0 && distanceToObject < closestObjectDistance)
                {
                    closestObject = sceneObject;
                    closestObjectDistance = distanceToObject;
                }
            }
            if (closestObject == null)
            {
                _distanceTravelled += double.MaxValue;
                return Settings.BackgroundColor;
            }
            _distanceTravelled += closestObjectDistance;
            return ComputeColor(ray, closestObject, ray.GetPositionAlongLine(closestObjectDistance));
        }

        private Color ComputeColor(Line3D ray, IObject3D collisionObject, Vector3D collisionPosition)
        {
            double currentDistance = _distanceTravelled;
            _currentRecoursion++;
            Color baseColor = collisionObject.CalculateColor(LightsNotInShadow(collisionPosition), collisionPosition,
                ray.DirectionVector);
            if (_currentRecoursion <= Settings.AmtOfRecoursions)
            {
                if (collisionObject.Shader.IsReflective())
                {
                    //generate the reflection ray and run CalculateObject Collisions again.

                    Line3D reflectRay = new Line3D(collisionPosition,
                        Vector3D.ReflectionVector(ray.DirectionVector,
                            collisionObject.CalculateNormVector(collisionPosition)));
                    Color reflectionColor = CalculateObjectCollisions(reflectRay);
                    baseColor = ColorToolbox.BlendSimpleByAmt(reflectionColor, baseColor,
                        collisionObject.Shader.Reflectivity);
                    _distanceTravelled = currentDistance;
                }
                if (collisionObject.Shader.IsRefractive())
                {
                    //generate the refraction ray and run CalculateObject Collisions again.
                    _distanceTravelled = currentDistance;
                }
            }
            _currentRecoursion--;
            return ColorToolbox.BlendSimpleByAmt(baseColor, Settings.BackgroundColor,
                1/Settings.DistanceInverseLaw(_distanceTravelled)); // 
        }

        private IList<ILight> LightsNotInShadow(Vector3D positionOnObject)
        {
            var tempList = new List<ILight>();
            foreach (var light in SceneLights)
            {
                // todo create fix such that the light can be local and between the objects.
                var lightVector = light.CalculateLightDirectionOnPosition(positionOnObject);
                var ray = new Line3D(positionOnObject, lightVector.VectorNegation());
                ray.PushStartPositionAlongLine(0.01);

                var softShadowRays = ray.Twist(Settings.ShadowRays, Settings.SoftShadowSpread);

                double intensity = light.Intensity;

                ray.PushStartPositionAlongLine(0.01);

                if (!(light is AmbientLight))
                {
                    foreach (var sceneObject in SceneObjects)
                    {
                        if (sceneObject is PlaneObject3D) continue;

                        // todo code does not work
                        int count = 0;
                        foreach (var shadowRay in softShadowRays)
                        {
                            count++;
                            if (sceneObject.CalculateCollisionPosition(shadowRay) > 0)
                            {
                                count = -softShadowRays.Count;
                                intensity -= light.Intensity/softShadowRays.Count;
                            }
                            else if (count > softShadowRays.Count/8)
                            {
                                break;
                            }
                        }
                    }
                }

                var lightWhichHits = light.Clone();
                lightWhichHits.Intensity = intensity;
                tempList.Add(lightWhichHits);
            }
            return tempList;
        }
    }
}