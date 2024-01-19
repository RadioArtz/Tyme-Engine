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
    internal class ArrayScene
    {
        private EngineWindow _engineWindow;
        public ArrayScene(EngineWindow window)
        {
            _engineWindow = window;
            CreateScene();
        }
        public void CreateScene()
        {
            GL.ClearColor(1,1,1, 1.0f);
            GameObject gameObject = new GameObject("ArrayManager");
            gameObject.AddComponent(new TransformComponent());
            gameObject.AddComponent(new ArrayDemoMgr());

            GameObject camera = new GameObject("MainCamera");
            camera.AddComponent(new TransformComponent());
            camera.AddComponent(new CameraComponent(10));
            camera._transformComponent.transform.Location = new Vector3((((ArrayDemoMgr)gameObject.childComponents[1]).arraySize-1)*0.75f, 64, 1024);
            camera._transformComponent.transform.Rotation = new Vector3(0,-90,0);
            camera.AddComponent(new EditorCamera(_engineWindow));
        }
    }
}
