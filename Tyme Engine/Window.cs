using Microsoft.VisualBasic;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using Tyme_Engine.Components;
using Tyme_Engine.IO;
using Tyme_Engine.Rendering;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace Tyme_Engine.Core
{
    class EngineWindow : GameWindow
    {
        public EngineWindow() : base(GameWindowSettings.Default,NativeWindowSettings.Default)
        {
        }
        private Matrix4 _projection;
        private Stopwatch _deltaCalc = new Stopwatch();
        private float _deltatime = 0.0f;

        #region WindowLoaded
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            //GL.Enable(EnableCap.FramebufferSrgb);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GameObject cube = new GameObject("TestObject0");
            //string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", "C:/Users/mathi/Downloads/spnz/sponza.obj");
            //string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", System.IO.Path.Combine(Environment.CurrentDirectory, "EngineContent/Meshes/sponza.obj"));
            //string input = Path.Combine(Environment.CurrentDirectory, "EngineContent/Meshes/cube.fbx");
            string input = "C:/Users/mathi/Documents/Sponza-master/Sponza-master/sponza.obj";
            //string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", "A:/Sponza/Main/Main/NewSponza_Main_FBX_YUp.fbx");
            //string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", System.IO.Path.Combine(Environment.CurrentDirectory,"EngineContent/Meshes/cube.fbx"));
            //string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", System.IO.Path.Combine(Environment.CurrentDirectory,"EngineContent/Meshes/shading_scene.fbx"));
            //string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", "C:/Users/mathi/Documents/sphere.fbx");
            GameObject camera = new GameObject("MainCamera");
            GameObject gameObject = new GameObject("pimel");

            gameObject.AddComponent(new TransformComponent());
            gameObject.AddComponent(new PointLightComponent());
            gameObject._transformComponent.transform.Location = new Vector3(0, 2, 0);
            gameObject.AddComponent(new Hoverlamp(1));

            camera.AddComponent(new TransformComponent());
            camera.AddComponent(new CameraComponent());
            camera.AddComponent(new EditorCamera(this));
            //camera.AddComponent(new PointLightComponent());
            
            cube.AddComponent(new StaticMeshComponent(AssetImporter.LoadMeshSync(input)));
            cube.AddComponent(new TransformComponent());
            cube.AddComponent(new TestScript(1));

            //testScene.SaveScene();
            //testScene.OpenScene();
            _deltaCalc.Start();
        }
        #endregion  

        #region LogicTick
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            _deltatime = (float)_deltaCalc.Elapsed.TotalSeconds;
            _deltaCalc.Restart();
            ScriptManager.ScriptUpdate((_deltatime));
        }
        #endregion
        public void SetShowMouseCursor(CursorState showCursor)
        {
            CursorState = showCursor;
        }
        public void SetCursorGrabbed(CursorState grabCursor)
        {
            CursorState = grabCursor;
        }

        #region RenderTick
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            ScriptManager.ScriptRender(_deltatime);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            RenderInterface.RenderStaticMeshes(RenderTime, _projection);
            Title = "DrawCalls:" + RenderInterface.drawcalls.ToString() + " FPS:" + Math.Round(1f / RenderTime).ToString() + " Vertices: " + RenderInterface.verticies.ToString() + " Faces: " + RenderInterface.Faces.ToString();
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
        #endregion

        #region Resizing
        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), (float)e.Width / (float)e.Height, 0.1f, 1024);
            base.OnResize(e);
        }
        #endregion
        #region UnloadWindow
        protected override void OnUnload()
        {
            foreach (GameObject obj in ObjectManager.GetAllObjects())
            {
                obj.DestroyObject();
            }
            base.OnUnload();
        }
        #endregion
    }
}