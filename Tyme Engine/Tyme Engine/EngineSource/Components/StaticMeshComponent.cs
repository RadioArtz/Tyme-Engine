using System;
using Tyme_Engine.Core;
using OpenTK.Graphics.OpenGL;
using Tyme_Engine.IO;
using Tyme_Engine.Rendering;
using System.IO;
using OpenTK;


namespace Tyme_Engine.Components
{
    [Serializable]
    class StaticMeshComponent : Component
    {
        //later replace this for a refference to the mesh in assetmanager
        [NonSerialized]
        private Assimp.Mesh loadedMesh;
        [NonSerialized]
        int VertexBufferObject;
        [NonSerialized]
        int VertexArrayObject;
        [NonSerialized]
        private Shader meshShader;
        
        private string meshPath;

        //Matrix4 transMatrix = Matrix4.CreateTranslation()
        /*
        public StaticMeshComponent(Assimp.Mesh assimpMesh, bool bShouldRender, bool bShouldShadow)
        {
            ChangeMesh(assimpMesh);
            //TODO: later have the component request the mesh to be loaded or sent over by the Asset Manager. for now we just load the mesh externally and then send it to the component.
        }*/

        public StaticMeshComponent(Assimp.Mesh assimpMesh)
        {
            ChangeMesh(assimpMesh);
        }

        //this'll make more sense later after above TODO is implemented.
        public void ChangeMesh(Assimp.Mesh assimpMesh)
        {
            loadedMesh = assimpMesh;
            var meshVerts = AssetImporter.ConvertVertecies(assimpMesh).ToArray();
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, meshVerts.Length * sizeof(float), meshVerts, BufferUsageHint.StaticDraw);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            meshShader = new Shader(Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/shader.vert"), Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/shader.frag"));
            meshShader.Use();

            foreach(Assimp.Vector3D vec in assimpMesh.Vertices)
            {

            }

        }

        internal void RenderMesh(double deltaTime, Matrix4 projection)
        {
            if (meshShader == null | parentObject._transformComponent == null)
            {
                Debug.Log("shader or Transform Component invalid");
                return;
            }
            meshShader.SetMatrix4("model", parentObject._transformComponent.GetModelMatrix());
            meshShader.SetMatrix4("view", Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f));
            meshShader.SetMatrix4("projection", projection);
            meshShader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, loadedMesh.Vertices.Count);
        }

        public override void OnComponentDestroyed()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            meshShader.Dispose();
        } 
    }
}