using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyme_Engine.Core;
using Tyme_Engine.Components;
using Tyme_Engine.IO;
using Tyme_Engine.Types;
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
        }

        public override void Update(float delta)
        { 
            parentObject._transformComponent.transform.Rotation = new Vector3(45f, parentObject._transformComponent.transform.Rotation.Y + delta * 50f, 0f);
            parentObject._staticMeshComponent.meshShader.SetVector4("tintColor", new Vector4(.6f, .3f, .1f, 1) * new Vector4((float)Math.Abs(Math.Sin(_timer.Elapsed.TotalSeconds))));
        }
        public override void PreRender(float delta)
        {
        }
    }
}