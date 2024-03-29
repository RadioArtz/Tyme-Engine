﻿using System;
using Tyme_Engine.Core;
using Tyme_Engine.Rendering;
using System.IO;
using OpenTK;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Tyme_Engine.Components
{
    [Serializable]
    class StaticMeshComponent : Component
    {
        //later replace this for a refference to the mesh in assetmanager
        public Shader meshShader { get; private set; }
        [NonSerialized]
        public Texture texture1;

        public List<Types.RuntimeStaticMesh> subMeshes = new List<Types.RuntimeStaticMesh>();
        public StaticMeshComponent(Assimp.Scene MeshScene)
        {
            //string input = Interaction.InputBox("Enter Texture file path", "Open Texture", Path.Combine(Environment.CurrentDirectory, "EngineContent/Textures/kenney_prototypetextures_png/Dark/texture_03.png"));
            string input = Path.Combine(Environment.CurrentDirectory, "EngineContent/Textures/kenney_prototypetextures_png/Dark/texture_03.png");
            texture1 = Texture.LoadFromFile(input,TextureMagFilter.Linear,TextureMinFilter.LinearMipmapLinear,16);
           
            meshShader = new Shader(Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/shader.vert"), Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/lit.frag"));
            
            meshShader.Use();

            foreach (Assimp.Mesh mesh in MeshScene.Meshes)
            {
                subMeshes.Add(new Types.RuntimeStaticMesh(mesh, texture1, meshShader));
            }
        }

        public void ChangeMesh(Assimp.Mesh assimpMesh)
        {
        }

        internal void RenderMesh(double deltaTime, Matrix4 projection, Matrix4 view)
        {
            foreach(Types.RuntimeStaticMesh mesh in subMeshes)
            {
                mesh.RenderMesh(deltaTime,projection,view, parentObject._transformComponent.GetModelMatrix());
            }
        }

        public override void OnComponentDestroyed()
        {
            foreach(Types.RuntimeStaticMesh mesh in subMeshes)
            {
                mesh.OnDestroyed();
            }
        } 
    }
}