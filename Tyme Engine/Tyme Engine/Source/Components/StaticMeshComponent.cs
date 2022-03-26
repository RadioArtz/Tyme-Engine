﻿using System;
using Tyme_Engine.Core;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Tyme_Engine.IO;
using Tyme_Engine.Rendering;
using System.IO;
namespace Tyme_Engine.Components
{
    class StaticMeshComponent : Component
    {
        //later replace this for a refference to the mesh in assetmanager
        private Assimp.Mesh loadedMesh;
        int VertexBufferObject;
        int VertexArrayObject;
        private Shader meshShader;

        float[] meshVerts = {
    -0.5f, -0.5f, 0.0f, //Bottom-left vertex
     0.5f, -0.5f, 0.0f, //Bottom-right vertex
     0.0f,  0.5f, 0.0f }; //Top vertex
                        //

        public StaticMeshComponent(Assimp.Mesh assimpMesh, bool bShouldRender, bool bShouldShadow)
        {
            ChangeMesh(assimpMesh);
            //TODO: later have the component request the mesh to be loaded or sent over by the Asset Manager. for now we just load the mesh externally and then send it to the component.
            
            
        }

        public StaticMeshComponent(Assimp.Mesh assimpMesh)
        {
            ChangeMesh(assimpMesh);
        }

        //this'll make more sense later after above TODO is implemented.
        public void ChangeMesh(Assimp.Mesh assimpMesh)
        {
            //loadedMesh = assimpMesh;
            //var meshVerts = AssetImporter.ConvertVertecies(assimpMesh).ToArray();
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, meshVerts.Length * sizeof(float), meshVerts, BufferUsageHint.StaticDraw);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            meshShader = new Shader(Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/shader.vert"), Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/shader.frag"));
            meshShader.Use();
        }

        internal void RenderMesh()
        {
            if (meshShader == null)
            {
                Debug.Log("shader invalid");
                return;
            }
            meshShader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }

        public override void OnComponentDestroyed(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
        } 

    }
}