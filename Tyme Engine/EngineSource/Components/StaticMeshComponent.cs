using System;
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
    public class StaticMeshComponent : Component
    {
        //later replace this for a reference to the mesh in assetmanager
        public Shader meshShader { get;  set; }
        [NonSerialized]
        public List<Types.RuntimeStaticMesh> subMeshes = new List<Types.RuntimeStaticMesh>();

        public StaticMeshComponent(Assimp.Scene MeshScene, bool unlit = false)
        {
            if(!unlit)
                meshShader = new Shader(Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/shader.vert"), Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/lit.frag"));
            if(unlit)
                meshShader = new Shader(Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/shader.vert"), Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/unlit.frag"));
            
            meshShader.Use();

            foreach (Assimp.Mesh mesh in MeshScene.Meshes)
            {
                subMeshes.Add(new Types.RuntimeStaticMesh(mesh, meshShader,unlit));
                Debug.Log(unlit,ConsoleColor.Red);
            }
        }
        public StaticMeshComponent(Assimp.Mesh InMesh, Assimp.Scene MeshScene,string path)
        {

            if(path.Contains("lava") || path.Contains("torch"))
            {
                meshShader = new Shader(Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/shader.vert"), Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/unlit_textured.frag"));
            }
            else
            {
                meshShader = new Shader(Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/shader.vert"), Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/lit.frag"));
            }

            meshShader.Use();
            subMeshes.Add(new Types.RuntimeStaticMesh(InMesh, meshShader, false));
        }

        public void ChangeMesh(Assimp.Mesh assimpMesh)
        { 

        }

        internal void RenderMesh(double deltaTime, Matrix4 projection, Matrix4 view)
        {
            if (IsPendingKill)
                return;

            foreach(Types.RuntimeStaticMesh mesh in subMeshes)
            {
                mesh.RenderMesh(deltaTime,projection,view, parentObject._transformComponent.GetModelMatrix());
            }
        }
        /*
        public override void OnComponentDestroyed()
        {
            foreach(Types.RuntimeStaticMesh mesh in subMeshes)
            {
                mesh.OnDestroyed();
            }
        }*/

        public override void OnKill()
        {
            
            base.OnKill();
            meshShader.Dispose();
            foreach (Types.RuntimeStaticMesh mesh in subMeshes)
            {
                mesh.OnDestroyed();
            }
            subMeshes.Clear();
        }
    }
}