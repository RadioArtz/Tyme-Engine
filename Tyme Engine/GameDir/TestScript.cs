﻿using Tyme_Engine.Core;
using OpenTK;
using System.Diagnostics;
using OpenTK.Input;

namespace Tyme_Engine
{
    class TestScript : UserScript   
    {
        private Stopwatch _timer = new Stopwatch();
        private float dir;
        public override void Start()
        {
            _timer.Start();
            this.parentObject._transformComponent.transform.Scale = new Vector3(.02f);
        }
        public TestScript(float direction)
        {
            dir = direction;    
        }
        public override void Update(float delta)
        { 
        }
        public override void PreRender(float delta)
        {
            KeyboardState input = Keyboard.GetState();
            if (input.IsKeyDown(Key.Enter))
            {
            }
                parentObject._staticMeshComponent.meshShader.SetVector3("DiffuseColor", new Vector3(1f, 1f, 1f));
                parentObject._staticMeshComponent.meshShader.SetVector3("SpecularColor", new Vector3(.125f, .125f, .125f));
                parentObject._staticMeshComponent.meshShader.SetVector3("LightColor", new Vector3(1f, 1f, 1f));
                parentObject._staticMeshComponent.meshShader.SetVector3("AmbientColor", new Vector3(.05f, .05f, .05f));
                parentObject._staticMeshComponent.meshShader.SetVector3("lightPos", Rendering.RenderInterface.hardcorelamp.parentObject._transformComponent.transform.Location);
                parentObject._staticMeshComponent.meshShader.SetVector3("viewPos", Rendering.RenderInterface._activeCamera.parentObject._transformComponent.transform.Location);
        }
    }
}