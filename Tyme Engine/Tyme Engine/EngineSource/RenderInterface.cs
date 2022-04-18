using System.Collections.Generic;
using System.Linq;
using Tyme_Engine.Components;
using Tyme_Engine.Core;
using OpenTK;
namespace Tyme_Engine.Rendering
{
    class Render3D
    {
        public static void RenderStaticMeshes(double delta, Matrix4 projection)
        {
            foreach(GameObject obj in ObjectManager.GetAllObjects())
            {
                if(obj._staticMeshComponent != null)
                    obj._staticMeshComponent.RenderMesh(delta, projection);
            }
        }
    }
}