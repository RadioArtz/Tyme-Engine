using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using Tyme_Engine.Components;
using Tyme_Engine.IO;
using Tyme_Engine.Rendering;

namespace Tyme_Engine.Core
{
    class EngineWindow : GameWindow
    {
        public EngineWindow(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
        }
        private Matrix4 projection;
        #region WindowLoaded
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            
            GameObject test0 = new GameObject("TestObject0");
            test0.AddComponent(new TestScript());

        }
        #endregion

        #region LogicTick
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            ScriptManager.TickScripts(UpdateTime);
        }
        #endregion

        #region RenderTick
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            Render3D.RenderStaticMeshes(RenderTime,projection);
            Context.SwapBuffers();
            Title = ((int)RenderFrequency).ToString();
            base.OnRenderFrame(e);
        }
        #endregion

        #region Resizing
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(80.0f), (float)Width/Height, 0.01f, 100.0f);
            base.OnResize(e);
        }
        #endregion

        #region UnloadWindow
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }
        #endregion
    }   
}