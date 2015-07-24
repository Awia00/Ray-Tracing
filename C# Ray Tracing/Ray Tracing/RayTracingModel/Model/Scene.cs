using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using RayTracingModel.Model.Cameras;
using RayTracingModel.Model.Lights;
using RayTracingModel.Model.Objects3D;

namespace RayTracingModel.Model
{
    internal class Scene
    {
        private bool test = true;

        [ThreadStatic] private static int _currentRecoursion;
        [ThreadStatic] private static double _distanceTravelled;
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

        async public Task<Color[,]> AsyncRender()
        {
            RenderProgress = 0;
            var cameraRays = Camera.GenerateCameraVectors();
            Color[,] colorArray = new Color[cameraRays.GetLength(0), cameraRays.GetLength(1)];
            double[,] distanceArray = new double[cameraRays.GetLength(0), cameraRays.GetLength(1)];

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
                var tasks = new List<Task<Tuple<int, int, double, Color>>>();
                for (int i = 0; i < colorArray.GetLength(0); i++)
                {
                    for (int j = 0; j < colorArray.GetLength(1); j++)
                    {
                        tasks.Add(AssignColor(i,j,cameraRays[i,j]));
                    }
                }
                foreach (var task in await Task.WhenAll(tasks))
                {
                    distanceArray[task.Item1, task.Item2] = task.Item3;
                    colorArray[task.Item1, task.Item2] = task.Item4;
                }
                RenderProgress = 1;
            }
            test = !test;
            colorArray = ApplyPostEffects(colorArray, distanceArray);
            return colorArray;
        }

        public Color[,] Render()
        {
            RenderProgress = 0;
            var cameraRays = Camera.GenerateCameraVectors();
            Color[,] colorArray = new Color[cameraRays.GetLength(0), cameraRays.GetLength(1)];
            double[,] distanceArray = new double[cameraRays.GetLength(0), cameraRays.GetLength(1)];

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
                for (int i = 0; i < colorArray.GetLength(0); i++)
                {
                    for (int j = 0; j < colorArray.GetLength(1); j++)
                    {
                        colorArray[i, j] = CalculateObjectCollisions(cameraRays[i, j]);
                        distanceArray[i,j] = _distanceTravelled;
                        RenderProgress += 1.0 / (colorArray.GetLength(0) * colorArray.GetLength(1));
                        _currentRecoursion = 0;
                        _distanceTravelled = 0;
                    }
                }
            }
            test = !test;
            colorArray = ApplyPostEffects(colorArray,distanceArray);
            RenderProgress = 1;
            return colorArray;
        }

        private Color[,] ApplyPostEffects(Color[,] colors, double[,] distances)
        {
            Color[,] colorsToReturn = ColorToolbox.ColorToolbox.SimpleBlur(colors, 0);
            foreach (PostEffect postEffect in Settings.PostEffects)
            {
                switch (postEffect)
                {
                    case PostEffect.DepthOfField:
                        int[,] distancesFixed = new int[distances.GetLength(0), distances.GetLength(1)];
                        for (int i = 0; i < distances.GetLength(0); i++)
                        {
                            for (int j = 0; j < distances.GetLength(1); j++)
                            {
                                if (distances[i, j] >= Settings.FocalNear && distances[i, j] <= Settings.FocalFar)
                                {
                                    distancesFixed[i, j] = 0;
                                }
                                else
                                {
                                    double absDistance = Math.Min(Math.Abs(Settings.FocalNear - distances[i, j]), Math.Abs(distances[i, j] - Settings.FocalFar));
                                    double radiusOfBlur = Math.Pow(absDistance, 0.5);
                                    if (radiusOfBlur > 25)
                                    {
                                        distancesFixed[i, j] = 25;
                                    }
                                    else
                                    {
                                        distancesFixed[i, j] = (int)radiusOfBlur;
                                    }
                                }
                                
                            }
                        }
                        colorsToReturn = ColorToolbox.ColorToolbox.SimpleBlur(colorsToReturn, distancesFixed);
                        break;
                    case PostEffect.DepthMap:
                        double maxDistance = distances.OfType<double>().Max();
                        for (int i = 0; i < distances.GetLength(0); i++)
                        {
                            for (int j = 0; j < distances.GetLength(1); j++)
                            {
                                int value = Math.Max(Math.Min((int)(distances[i, j]/maxDistance*255),255),0);
                                colorsToReturn[i, j] = Color.FromArgb(255, value, value, value);
                            }
                        }
                        break;
                }
            }
            return colorsToReturn;
        }

        async private Task<Tuple<int,int,double,Color>> AssignColor(int i, int j, Line3D ray)
        {
            Func<Color> tempColorFunc = () => CalculateObjectCollisions(ray);
            var tempColor = await Task.Run(tempColorFunc);
            RenderProgress += 1.0 / (Camera.AmtOfHeightPixels * Camera.AmtOfWidthPixels);
            var tuple = Tuple.Create(i, j,_distanceTravelled, tempColor);
            _currentRecoursion = 0;
            _distanceTravelled = 0;
            return tuple;
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
            Vector3D normalVector = collisionObject.CalculateNormVector(collisionPosition);
            _currentRecoursion++;

            Color baseColor = collisionObject.Shader.CalculateColor(LightsNotInShadow(collisionPosition), normalVector,
                ray.DirectionVector,collisionPosition);

            if (_currentRecoursion <= Settings.AmtOfRecoursions)
            {
                
                if (collisionObject.Shader.IsReflective())
                {
                    //generate the reflection ray and run CalculateObject Collisions again.

                    Line3D reflectRay = new Line3D(collisionPosition,
                        Vector3D.ReflectionVector(ray.DirectionVector,
                            normalVector));
                    
                    Color reflectionColor = CalculateObjectCollisions(reflectRay);
                    
                    baseColor = ColorToolbox.ColorToolbox.BlendSimpleByAmt(reflectionColor, baseColor,
                        collisionObject.Shader.Reflectivity);
                    _distanceTravelled = currentDistance;
                }
                if (collisionObject.Shader.IsRefractive())
                {
                    try
                    {
                        double n1 = Settings.SpaceRefractionIndex;
                        double n2 = collisionObject.Shader.RefractionIndex;
                        Vector3D refractionVector;
                        if (Vector3D.DotProdukt(normalVector, ray.DirectionVector) < 0)
                        {
                            refractionVector = Vector3D.RefractionVector(ray.DirectionVector, normalVector, n1, n2);
                        }
                        else
                        {
                            refractionVector = Vector3D.RefractionVector(ray.DirectionVector, normalVector.VectorNegation(), n2, n1);
                        }
                        
                        Line3D refractionRay = new Line3D(collisionPosition, refractionVector);

                        Color refractionColor = CalculateObjectCollisions(refractionRay);

                        baseColor = ColorToolbox.ColorToolbox.BlendSimpleByAmt(refractionColor, baseColor,
                        collisionObject.Shader.Refractivity);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("hallo du");
                    }
                    _distanceTravelled = currentDistance;
                }
            }
            _currentRecoursion--;
            return ColorToolbox.ColorToolbox.BlendSimpleByAmt(baseColor, Settings.BackgroundColor,
                Settings.DistanceInverseLawCamera(_distanceTravelled)); // 
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
                intensity /= Settings.DistanceInverseLawLight(light.DistanceFromLight(positionOnObject));

                ray.PushStartPositionAlongLine(0.01);

                if (!(light is AmbientLight))
                {
                    foreach (var sceneObject in SceneObjects)
                    {
                        if (sceneObject is PlaneObject3D) continue;
                        if (sceneObject.Shader.IsRefractive()) continue;
                        int count = 0;
                        foreach (var shadowRay in softShadowRays)
                        {
                            count++;
                            double distanceToCollision = sceneObject.CalculateCollisionPosition(shadowRay);
                            if (distanceToCollision > 0 && Vector3D.IsSameDirection(lightVector, light.CalculateLightDirectionOnPosition(shadowRay.GetPositionAlongLine(distanceToCollision))))
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