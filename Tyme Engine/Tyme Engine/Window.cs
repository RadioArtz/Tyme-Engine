using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace Tyme_Engine.Engine
{
    class EngineWindow : GameWindow
    {
        private Components.StaticMesh testThing;
        Camera MainCamera;
        public EngineWindow(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
        }

        #region WindowLoaded
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            MainCamera = new Camera();
            MainCamera.Rotation = new Vector3(0, 90, 0);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            var tmpmesh = IO.AssetImporter.GetMesh("C:/Users/mathi/Documents/cube.fbx");
            testThing = new Components.StaticMesh(tmpmesh,true,true);
        }
        #endregion

        #region PreRenderTick
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused) // check to see if the window is focused
            {
                return;
            }
            KeyboardState input = Keyboard.GetState();
            MainCamera.DoInput();
            if (input.IsKeyDown(Key.W))
            {
                MainCamera.Position += MainCamera.ForwardVector * MainCamera.speed * (float)e.Time; //Forward 
            }

            if (input.IsKeyDown(Key.S))
            {
                MainCamera.Position -= MainCamera.ForwardVector * MainCamera.speed * (float)e.Time; //Backwards
            }

            if (input.IsKeyDown(Key.A))
            {
                MainCamera.Position += MainCamera.RightVector * MainCamera.speed * (float)e.Time; //Left
            }

            if (input.IsKeyDown(Key.D))
            {
                MainCamera.Position -= MainCamera.RightVector * MainCamera.speed * (float)e.Time; //Right
            }

            if (input.IsKeyDown(Key.Space))
            {
                MainCamera.Position += MainCamera.UpVector * MainCamera.speed * (float)e.Time; //Up 
            }

            if (input.IsKeyDown(Key.LShift))
            {
                MainCamera.Position -= MainCamera.UpVector * MainCamera.speed * (float)e.Time; //Down
            }
        }
        #endregion

        #region RenderTick
        //General rendering shenanigans go here
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //put rendering here
            Console.WriteLine(MainCamera.Rotation);
            List<Components.StaticMesh> templist = new List<Components.StaticMesh>();
            templist.Add(testThing);
            MainCamera.UpdateVectors();
            Rendering.Render3D.RenderStaticMeshes(templist,MainCamera);
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