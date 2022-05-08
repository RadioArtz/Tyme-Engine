﻿using Microsoft.VisualBasic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;
using Tyme_Engine.Components;
using Tyme_Engine.IO;
using Tyme_Engine.Rendering;

namespace Tyme_Engine.Core
{
    class EngineWindow : GameWindow
    {
        public EngineWindow(int width, int height, string title) : base(width, height, GraphicsMode.Default, title,GameWindowFlags.Default,DisplayDevice.Default,3,1,GraphicsContextFlags.ForwardCompatible)
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
            GL.Enable(EnableCap.CullFace);
            //GL.Enable(EnableCap.FramebufferSrgb);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);
            Scene testScene = new Scene();
            GameObject cube = new GameObject("TestObject0");
            //string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", "C:/Users/mathi/Downloads/spnz/sponza.obj");
            string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", System.IO.Path.Combine(Environment.CurrentDirectory, "EngineContent/Meshes/sponza.obj"));
            //string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", "A:/Sponza/Main/Main/NewSponza_Main_FBX_YUp.fbx");
            //string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", System.IO.Path.Combine(Environment.CurrentDirectory,"EngineContent/Meshes/cube.fbx"));
            //string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", System.IO.Path.Combine(Environment.CurrentDirectory,"EngineContent/Meshes/shading_scene.fbx"));
            //string input = Interaction.InputBox("Enter Mesh file path", "Open Mesh", "C:/Users/mathi/Documents/sphere.fbx");
            GameObject camera = new GameObject("MainCamera");
            
            camera.AddComponent(new TransformComponent());
            camera.AddComponent(new CameraComponent());
            camera.AddComponent(new EditorCamera(this));
            camera.AddComponent(new PointLampComponent());
            
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
        public void SetShowMouseCursor(bool showCursor)
        {
            CursorVisible = showCursor;
        }
        public void SetCursorGrabbed(bool grabCursor)
        {
            CursorGrabbed = grabCursor;
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
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), (float)Width / Height, 0.1f, 512.0f);
            base.OnResize(e);
        }
        #endregion

        #region UnloadWindow
        protected override void OnUnload(EventArgs e)
        {
            foreach (GameObject obj in ObjectManager.GetAllObjects())
            {
                obj.DestroyObject();
            }
            base.OnUnload(e);
        }
        #endregion
    }
}