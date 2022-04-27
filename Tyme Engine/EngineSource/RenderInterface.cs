﻿using System.Collections.Generic;
using Tyme_Engine.Components;
using Tyme_Engine.Core;
using OpenTK;

namespace Tyme_Engine.Rendering
{
    class RenderInterface
    {
        public static List<CameraComponent> _cameras { get; private set; } = new List<CameraComponent>();
        public static CameraComponent _activeCamera { get; private set; }
        private static int drawcalls;
        public static PointLampComponent hardcorelamp;
        public static void RenderStaticMeshes(double delta, Matrix4 projection, GameWindow window)
        {
            drawcalls = 0;
            _activeCamera.UpdateViewMatrix();
            foreach(GameObject obj in ObjectManager.GetAllObjects())
            {
                if (obj._staticMeshComponent != null)
                    {
                        obj._staticMeshComponent.RenderMesh(delta, projection, _activeCamera.view);
                        drawcalls += obj._staticMeshComponent.subMeshes.Count;
                    }
            }
            window.Title = drawcalls.ToString();
        }

        public static void AddCamera(CameraComponent cameraComponent)
        {
            _cameras.Add(cameraComponent);
            if (_activeCamera == null | _cameras.Count<0)
            {
                _activeCamera = _cameras[0];
            }
        }

        public static void SetActiveCamera(CameraComponent cameraComponent)
        {
            _activeCamera = cameraComponent;
        }
    }
}