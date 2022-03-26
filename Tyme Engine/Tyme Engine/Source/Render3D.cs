using System.Collections.Generic;

namespace Tyme_Engine.Rendering
{
    class Render3D
    {
        
        public static void RenderStaticMeshes()
        {
            foreach(Tyme_Engine.Core.GameObject obj in Tyme_Engine.ObjectManager.GetAllObjects())
            {
                foreach(Tyme_Engine.Components.StaticMeshComponent statcomp in obj.GetComponents())
                {
                    statcomp.RenderMesh();
                }
            }
        }
    }
}