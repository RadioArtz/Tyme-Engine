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
            this.parentObject._transformComponent.transform.Scale = new Vector3(0.75f);
        }

        public override void Update(float delta)
        { 
            parentObject._transformComponent.transform.Rotation = new Vector3(-90,0, 0f);
            //parentObject._transformComponent.transform.Location = new Vector3(0, (float)Math.Sin(_timer.Elapsed.TotalSeconds), 0);
            foreach(Types.RuntimeStaticMesh mesh in parentObject._staticMeshComponent.subMeshes)
            {
                //mesh.meshShader.SetVector4("tintColor", new Vector4(.6f, .3f, .1f, 1) * new Vector4((float)Math.Abs(Math.Sin(_timer.Elapsed.TotalSeconds))));
                mesh.meshShader.SetVector4("tintColor", new Vector4(.6f, .3f, .1f, 1));
            }
            //if(OpenTK.Input.key)
        }
        public override void PreRender(float delta)
        {
        }
    }
}