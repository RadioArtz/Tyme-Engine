using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace Tyme_Engine.Rendering
{
    class Render3D
    {
        public static void RenderStaticMeshes(List<Tyme_Engine.Components.StaticMesh> StaticMeshes, Camera cam)
        {

            foreach(Tyme_Engine.Components.StaticMesh sm in StaticMeshes)
            {
                sm.RenderMesh(cam.view);
            }
        }
    }
}