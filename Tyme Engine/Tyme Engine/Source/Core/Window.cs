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

        #region WindowLoaded
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            GameObject test = new GameObject("TestObject");
            test.AddComponent(new StaticMeshComponent(AssetImporter.LoadMeshSync("C:/Users/mathi/Documents/Cube.fbx")));
            test.AddComponent(new TransformComponent());
        }
        #endregion

        #region LogicTick
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
        }
        #endregion

        #region RenderTick
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            Render3D.RenderStaticMeshes(.01f);
            Context.SwapBuffers();
            this.Title = ((int)this.RenderFrequency).ToString();
            base.OnRenderFrame(e);
        }
        #endregion

        #region Resizing
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
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