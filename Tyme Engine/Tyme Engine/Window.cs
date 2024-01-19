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
        
        public EngineWindow(NativeWindowSettings settings) : base(GameWindowSettings.Default,settings)
        {
        }
        private Matrix4 _projection;
        private Stopwatch _deltaCalc = new Stopwatch();
        private float _deltatime = 0.0f;
        private float _cachedFOV = 90f;
        #region WindowLoaded
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Multisample);
            GL.Enable(EnableCap.FramebufferSrgb);
            
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);

            ArrayScene arrscn = new ArrayScene(this);
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
            RebuildProjectionMatrix(_cachedFOV);
            base.OnResize(e);
        }

        //No need to rebuild the projection matrix every frame unless we're changing FOV or window wize so we might as well cache it to save some (miniscule) CPU time
        public void RebuildProjectionMatrix(float FieldOfViewDeg)
        {
            _cachedFOV = FieldOfViewDeg;
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(_cachedFOV), (float)this.Size.X / (float)this.Size.Y, 0.1f, 2048);
            Debug.Log(this.Size, ConsoleColor.Cyan);
        }
        #endregion
        #region UnloadWindow
        protected override void OnUnload()
        {/*
            foreach (GameObject obj in ObjectManager.GetAllObjects())
            {
                obj.DestroyObject();
            }*/
            base.OnUnload();
        }
        #endregion
    }
}