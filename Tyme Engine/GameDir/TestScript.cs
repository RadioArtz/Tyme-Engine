using System;
using Tyme_Engine.Core;
using OpenTK;
using System.Diagnostics;
namespace Tyme_Engine
{
    class TestScript : UserScript
    {
        private Stopwatch _timer = new Stopwatch();
        //private GameObject leepic;
        public override void Start()
        {
            //leepic = new GameObject("TestObject");
            //leepic.AddComponent(new StaticMeshComponent(AssetImporter.LoadMeshSync("C:/Users/mathi/Documents/Cube.fbx")));
            //leepic.AddComponent(new TransformComponent());
            _timer.Start();
            this.parentObject._transformComponent.transform.Scale = new Vector3(.01f);
        }

        public override void Update(float delta)
        { 
            //parentObject._transformComponent.transform.Rotation = new Vector3(-90,0, 0f);
            //parentObject._transformComponent.transform.Location = new Vector3(0, (float)Math.Sin(_timer.Elapsed.TotalSeconds), 0);
            //parentObject._transformComponent.transform.Rotation = new Vector3(-90f, parentObject._transformComponent.transform.Rotation.Y + delta * 5f, 0f);

            
        }
        public override void PreRender(float delta)
        {
                parentObject._staticMeshComponent.meshShader.SetVector3("DiffuseColor", new Vector3(1.0f, 0.5f, 0.31f));
                //parentObject._staticMeshComponent.meshShader.SetVector3("SpecularColor", new Vector3(0.0f, 0.0f, 0.0f));
                parentObject._staticMeshComponent.meshShader.SetVector3("LightColor", new Vector3(1.0f, 1.0f, 1.0f));
                parentObject._staticMeshComponent.meshShader.SetVector3("AmbientColor", new Vector3(.1f, .1f, .1f));
                parentObject._staticMeshComponent.meshShader.SetVector3("lightPos", Rendering.RenderInterface.hardcorelamp.parentObject._transformComponent.transform.Location);
                //Core.Debug.Log(parentObject._staticMeshComponent.meshShader.GetAttribLocation("lightPos"));
                //parentObject._staticMeshComponent.meshShader.SetVector3("lightPos", new Vector3(0,2,-5));
        }
    }
}