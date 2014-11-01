﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingModel.Model.Cameras
{
    interface ICamera
    {
        Vector3D[,] GenerateCameraVectors();
    }
}
