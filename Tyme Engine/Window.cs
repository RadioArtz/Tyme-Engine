using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using Tyme_Engine.Components;
using Tyme_Engine.IO;
using Tyme_Engine.Rendering;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.IO;

namespace Tyme_Engine.Core
{
    class EngineWindow : GameWindow
    {
        public EngineWindow(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
        }
        private Matrix4 _projection;
        
        #region WindowLoaded
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            Scene testScene = new Scene();
            GameObject cube = new GameObject("TestObject0");
            string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", Path.Combine(Environment.CurrentDirectory,"EngineContent/Meshes/cube.fbx"));
            cube.AddComponent(new StaticMeshComponent(AssetImporter.LoadMeshSync(input)));
            cube.AddComponent(new TransformComponent());
            cube.AddComponent(new TestScript());

            GameObject camera = new GameObject("MainCamera");
            camera.AddComponent(new TransformComponent());
            camera.AddComponent(new CameraComponent());
            camera.AddComponent(new CameraZoomTest());
            //testScene.SaveScene();
            //testScene.OpenScene();
        }
        #endregion

        #region LogicTick
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            ScriptManager.ScriptUpdate(((float)UpdatePeriod));
        }
        #endregion

        #region RenderTick
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            ScriptManager.ScriptRender((float)UpdatePeriod);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            RenderInterface.RenderStaticMeshes(RenderTime,_projection);
            Context.SwapBuffers();
            //Title = (Math.Round(1 / _deltatime)).ToString();
            //Debug.Log("fps: " + (Math.Round(1 / _deltatime)).ToString() + " deltatime: " + _deltatime.ToString());
            Title = (1/UpdatePeriod).ToString();
            base.OnRenderFrame(e);
        }
        #endregion

        #region Resizing
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(80.0f), (float)Width/Height, 0.01f, 100.0f);
            base.OnResize(e);
        }
        #endregion

        #region UnloadWindow
        protected override void OnUnload(EventArgs e)
        {
            foreach(GameObject obj in ObjectManager.GetAllObjects())
            {
                obj.DestroyObject();
            }
            base.OnUnload(e);
        }
        #endregion
    }   
}