using System;
using Tyme_Engine.Core;
using OpenTK.Graphics.OpenGL;
using Tyme_Engine.IO;
using Tyme_Engine.Rendering;
using System.IO;
using OpenTK;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace Tyme_Engine.Types
{
    class RuntimeStaticMesh
    {
        //later replace this for a refference to the mesh in assetmanager
        [NonSerialized]
        public Assimp.Mesh loadedMesh;
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
        public Texture texture1;

        //Matrix4 transMatrix = Matrix4.CreateTranslation()
        /*
        //more advanced import method to pass initial settings, might scrap this but will keep it for the moment.
        public StaticMeshComponent(Assimp.Mesh assimpMesh, bool bShouldRender, bool bShouldShadow)
        {
            ChangeMesh(assimpMesh);
            //TODO: later have the component request the mesh to be loaded or sent over by the Asset Manager. for now we just load the mesh externally and then send it to the component.
        }*/

        public RuntimeStaticMesh(Assimp.Mesh assimpMesh, Texture tex, Shader inshader)
        {
            texture1 = tex;
            meshShader = inshader;
            // Core.Debug.Log(texture1 == null);
            ChangeMesh(assimpMesh);
        }

        //this'll make more sense later after above TODO is implemented.
        public void ChangeMesh(Assimp.Mesh assimpMesh)
        {
            loadedMesh = assimpMesh;
            var meshVerts = AssetImporter.ConvertVertecies(assimpMesh, true, true, 0);
            var meshIndecies = AssetImporter.ConvertIndecies(assimpMesh);
            indeciesCount = meshIndecies.Length;
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, meshVerts.Length * sizeof(float), meshVerts, BufferUsageHint.StaticDraw);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            int vertPosLocation = meshShader.GetAttribLocation("aPos");
            GL.VertexAttribPointer(vertPosLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(vertPosLocation);
            
            int texCoordLocation = meshShader.GetAttribLocation("aTexCoord");
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(texCoordLocation);

            int normalLocation = meshShader.GetAttribLocation("aNormal");
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 5 * sizeof(float));
            GL.EnableVertexAttribArray(normalLocation);
            
            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indeciesCount * sizeof(uint), meshIndecies, BufferUsageHint.StaticDraw);

            texture1.Use(TextureUnit.Texture0);
            meshShader.SetInt("texture0", 0);

            //meshShader.SetVector4("tintColor", new Vector4(0.6f, 0.3f, 0.1f, 1));
            meshShader.Use();
        }

        internal void RenderMesh(double deltaTime, Matrix4 projection, Matrix4 view, Matrix4 model)
        {
            GL.BindVertexArray(VertexArrayObject);
            if (meshShader == null)
            {
                Core.Debug.Log("shader invalid");
                return;
            }
            meshShader.SetMatrix4("model", model);
            meshShader.SetMatrix4("view", view);
            meshShader.SetMatrix4("projection", projection);
            meshShader.Use();
            GL.DrawElements(PrimitiveType.Triangles, indeciesCount, DrawElementsType.UnsignedInt, 0);
        }

        public void OnDestroyed()
        {
            GL.DeleteBuffer(VertexBufferObject);
            GL.DeleteBuffer(VertexArrayObject);
            GL.DeleteBuffer(ElementBufferObject);

            meshShader.Dispose();
        }
    }
}
