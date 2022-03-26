using System;
using Tyme_Engine.Core;

namespace Tyme_Engine.Components
{
    class StaticMeshComponent : Component
    {
        private Assimp.Mesh loadedMesh;
        
        public StaticMeshComponent(Assimp.Mesh assimpMesh, bool bShouldRender, bool bShouldShadow)
        {
            //TODO: later have the component request the mesh to be loaded or sent over by the Asset Manager. for now we just load the mesh externally and then send it to the component.
            ChangeMesh(assimpMesh);
        }

        public StaticMeshComponent(Assimp.Mesh assimpMesh)
        {
            ChangeMesh(assimpMesh);
        }

        //this'll make more sense later after above TODO is implemented.
        public void ChangeMesh(Assimp.Mesh assimpMesh)
        {
            loadedMesh = assimpMesh;
        }

        internal void RenderMesh()
        {
            Console.WriteLine(loadedMesh.Name);
        }
    }
}