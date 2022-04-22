using System;
using Tyme_Engine.Core;
using OpenTK.Graphics.OpenGL;
using Tyme_Engine.IO;
using Tyme_Engine.Rendering;
using System.IO;
using OpenTK;
using Microsoft.VisualBasic;
using System.Diagnostics;
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
        public Shader meshShader { get; private set; }
        [NonSerialized]
        private int ElementBufferObject;
        [NonSerialized]
        private int indeciesCount;
        [NonSerialized]
        Texture texture1;
        public float scrollvalue = 0f;
        private string meshPath;
        
        //Matrix4 transMatrix = Matrix4.CreateTranslation()
        /*
        //more advanced import method to pass initial settings, might scrap this but will keep it for the moment.
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
            var meshVerts = AssetImporter.ConvertVertecies(assimpMesh,true,0);
            var meshIndecies = AssetImporter.ConvertIndecies(assimpMesh);
            indeciesCount = meshIndecies.Length;
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, meshVerts.Length * sizeof(float), meshVerts, BufferUsageHint.StaticDraw);
            
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            meshShader = new Shader(Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/shader.vert"), Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/unlit.frag"));
            meshShader.Use();

            int vertPosLocation = meshShader.GetAttribLocation("aPos");
            GL.VertexAttribPointer(vertPosLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(vertPosLocation);

            int texCoordLocation = meshShader.GetAttribLocation("aTexCoord");
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(texCoordLocation);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indeciesCount * sizeof(uint), meshIndecies, BufferUsageHint.StaticDraw);

            string input = Interaction.InputBox("Enter Texture file path", "Open Texture", "C:/Program Files (x86)/World Machine Basic/resources/grid.bmp");
            texture1 = Texture.LoadFromFile(input);
            texture1.Use(TextureUnit.Texture0);

            meshShader.SetInt("texture0", 0);
            meshShader.SetVector4("tintColor", new Vector4(0.6f, 0.3f, 0.1f, 1));
            meshShader.Use();
        }

        internal void RenderMesh(double deltaTime, Matrix4 projection)
        {
            if (meshShader == null | parentObject._transformComponent == null)
            {
                //Debug.Log("shader or Transform Component invalid");
                return;
            }
            meshShader.SetMatrix4("model", parentObject._transformComponent.GetModelMatrix());
            meshShader.SetMatrix4("view", Matrix4.CreateTranslation(0.0f, 0.0f, scrollvalue));
            meshShader.SetMatrix4("projection", projection);
            meshShader.Use();
            GL.BindVertexArray(VertexArrayObject);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, loadedMesh.Vertices.Count);
            GL.DrawElements(PrimitiveType.Triangles, indeciesCount, DrawElementsType.UnsignedInt, 0);
        }

        public override void OnComponentDestroyed()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            meshShader.Dispose();
        } 
    }
}