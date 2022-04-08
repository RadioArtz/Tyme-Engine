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
namespace Tyme_Engine
{
    class TestScript : UserScript
    {
        private GameObject leepic;
        public override void Start()
        {
            leepic = new GameObject("TestObject");
            leepic.AddComponent(new StaticMeshComponent(AssetImporter.LoadMeshSync("C:/Users/mathi/Documents/Cube.fbx")));
            leepic.AddComponent(new TransformComponent());
        }

        public override void Tick(double delta)
        {
            float deltatime = (float)delta;
            leepic._transformComponent.transform.Rotation = new Vector3(45, leepic._transformComponent.transform.Rotation.Y + deltatime * 125f, 75);
            Debug.Log("Update Time: " + deltatime);
        }
    }
}