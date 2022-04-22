using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using Tyme_Engine.Components;
using Tyme_Engine.IO;
using Tyme_Engine.Rendering;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace Tyme_Engine.Core
{
    class EngineWindow : GameWindow
    {
        public EngineWindow(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
        }
        private Matrix4 _projection;
        private Stopwatch _deltaCalc = new Stopwatch();
        private float _deltatime = 0.0f;
        
        #region WindowLoaded
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            Scene testScene = new Scene();
            GameObject test0 = new GameObject("TestObject0");
            string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", "C:/Users/mathi/Documents/Cube.fbx");
            test0.AddComponent(new StaticMeshComponent(AssetImporter.LoadMeshSync(input)));
            test0.AddComponent(new TransformComponent());
            test0.AddComponent(new TestScript());
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
            OpenTK.Input.MouseState scroll = OpenTK.Input.Mouse.GetState();
            foreach(GameObject obj in ObjectManager.objectBuffer)
            {
                obj._staticMeshComponent.scrollvalue = MathExt.Lerp(obj._staticMeshComponent.scrollvalue, scroll.WheelPrecise, MathExt.Clamp01(_deltatime * 16f));
            }
        }
        #endregion

        #region RenderTick
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            ScriptManager.ScriptRender(_deltatime);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            Render3D.RenderStaticMeshes(RenderTime,_projection);
            Context.SwapBuffers();
            Title = ((int)RenderFrequency).ToString();
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