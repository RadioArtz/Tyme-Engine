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
using OpenTK.Graphics.OpenGL;

namespace Tyme_Engine
{
    internal class TestScene2
    {
        private EngineWindow _engineWindow;
        public TestScene2(EngineWindow window)
        {
            _engineWindow = window;
            CreateScene();
        }
        public void CreateScene()
        {

            string input = @"C:\Users\mathi\Documents\Sponza-master\Sponza-master/sponza.obj";
            //string input2 = Path.Combine(Environment.CurrentDirectory, "EngineContent/Meshes/shading_scene.fbx");
            //string input = @"A:\scp_room_1/scene.gltf";
            GameObject camera = new GameObject("MainCamera");
            GameObject gameObject = new GameObject("lightObject");
            gameObject.AddComponent(new TransformComponent());
            
            gameObject.AddComponent(new PointLightComponent());
            gameObject._transformComponent.transform.Location = new Vector3(0, 2, 0);
            gameObject.AddComponent(new Hoverlamp(1));
            
            camera.AddComponent(new TransformComponent());
            camera.AddComponent(new CameraComponent());
            camera.AddComponent(new EditorCamera(_engineWindow));
            camera._transformComponent.transform.Location = new Vector3(0, 32, -16);
            //camera.AddComponent(new PointLightComponent());


            var cube = new NullableWrapper<GameObject>(new GameObject("cock"));
            cube.Value.AddComponent(new TransformComponent());
            cube.Value.AddComponent(new StaticMeshComponent(AssetImporter.LoadMeshSync(input)));
            Debug.Log(cube.IsValid(),ConsoleColor.Green);
            //cube.Invalidate();
            //cube.Value._staticMeshComponent.MarkPendingKill();
            
            //cube.Value.childComponents.Add();
            Debug.Log(cube.IsValid(), ConsoleColor.Red);




            /*
            cube.AddComponent(new StaticMeshComponent(AssetImporter.LoadMeshSync(input)));
            cube.AddComponent(new TransformComponent());
            cube.AddComponent(new TestScript(1));*/


            /*
            foreach(GameObject obj in ObjectManager.GetAllObjects())
            {
                obj.DestroyObject();
            }*/

            //GameObject cube2;
            //cube2 = EditorHelpers.CreateMeshFromMemory(0,input);
            //Debug.Log(cube.objectName);

            //cube._staticMeshComponent.meshShader = new Shader(Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/shader.vert"), Path.Combine(Environment.CurrentDirectory, "EngineContent/Shaders/unlit.frag"));

            //testScene.SaveScene();
            //testScene.OpenScene();
        }

    }
}
