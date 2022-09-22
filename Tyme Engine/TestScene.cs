using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyme_Engine.Components;
using Tyme_Engine.Core;
using OpenTK.Mathematics;
using Tyme_Engine.IO;
using Tyme_Engine.Rendering;
using Tyme_Engine.Editor;

namespace Tyme_Engine
{
    internal class TestScene
    {
        private EngineWindow _engineWindow;
        public TestScene(EngineWindow window)
        {
            _engineWindow = window;
            CreateScene();
        }
        public void CreateScene()
        {
            GameObject cube;// = new GameObject("TestObject0");
            string input = "A:/scp_room_1/scene.gltf";
            //string input = Path.Combine(Environment.CurrentDirectory, "EngineContent/Meshes/shading_scene.fbx");

            GameObject camera = new GameObject("MainCamera");
            GameObject gameObject = new GameObject("pimel");
            gameObject.AddComponent(new TransformComponent());
            gameObject.AddComponent(new PointLightComponent());
            gameObject._transformComponent.transform.Location = new Vector3(0, 2, 0);
            gameObject.AddComponent(new Hoverlamp(1));

            camera.AddComponent(new TransformComponent());
            camera.AddComponent(new CameraComponent());
            camera.AddComponent(new EditorCamera(_engineWindow));
            //camera.AddComponent(new PointLightComponent());

            /*
            cube.AddComponent(new StaticMeshComponent(AssetImporter.LoadMeshSync(input)));
            cube.AddComponent(new TransformComponent());
            cube.AddComponent(new TestScript(1));*/

            cube = EditorHelpers.CreateMeshFromFile(input);
            //Debug.Log(cube.objectName);

            //cube._staticMeshComponent.meshShader = new Shader(Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/shader.vert"), Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/unlit.frag"));

            //testScene.SaveScene();
            //testScene.OpenScene();
        }

    }
}
