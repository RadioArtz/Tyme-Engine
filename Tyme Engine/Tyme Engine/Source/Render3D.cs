using System.Collections.Generic;
using System.Linq;
using Tyme_Engine.Components;
using Tyme_Engine.Core;

namespace Tyme_Engine.Rendering
{
    class Render3D
    {
        public static void RenderStaticMeshes(float deltaTime)
        {
            foreach(GameObject obj in ObjectManager.GetAllObjects())
            {
                if(obj.staticMeshComponent != null)
                    obj.staticMeshComponent.RenderMesh(deltaTime);
            }
        }
    }
}