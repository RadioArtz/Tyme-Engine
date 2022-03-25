using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using Tyme_Engine.Components;
using Tyme_Engine.IO;

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
            GameObject test = new GameObject("fette komponentierung");
            test.AddComponent(new StaticMeshComponent(AssetImporter.LoadMeshSync("C:/Users/mathi/Documents/cube.fbx")));
            test.AddComponent(new TransformComponent());
        }
        #endregion

        #region PreRenderTick
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
        }
        #endregion

        #region RenderTick
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            Context.SwapBuffers();
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
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            base.OnUnload(e);
        }
        #endregion
    }   
}