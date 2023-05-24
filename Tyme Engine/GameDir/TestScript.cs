using Tyme_Engine.Core;
using OpenTK;
using System.Diagnostics;
using OpenTK.Input;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics;

namespace Tyme_Engine
{
    class TestScript : UserScript   
    {
        private Stopwatch _timer = new Stopwatch();
        private float dir;
        public override void Start()
        {
            _timer.Start();
            int rand = new Random().Next(8)-4;
            this.parentObject._transformComponent.transform.Scale = new Vector3(.2f);
            this.parentObject._transformComponent.transform.Location = new Vector3(0, 0, 0);
            parentObject._transformComponent.GetModelMatrix(true);
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
                Rendering.RenderInterface._hardcorelamp._radius = MathHelper.Clamp(Rendering.RenderInterface._hardcorelamp._radius, 0, Rendering.RenderInterface._hardcorelamp._radius);
                parentObject._staticMeshComponent.meshShader.SetVector3("DiffuseColor", new Vector3(1f, 1f, 1f));
                parentObject._staticMeshComponent.meshShader.SetVector3("SpecularColor", new Vector3(1f, 1f, 1f));
                parentObject._staticMeshComponent.meshShader.SetVector4("LightColor", Rendering.RenderInterface._hardcorelamp._lightColor);
                parentObject._staticMeshComponent.meshShader.SetVector3("AmbientColor", new Vector3(.05f, .05f, .05f)/1);
                parentObject._staticMeshComponent.meshShader.SetVector3("lightPos", Rendering.RenderInterface._hardcorelamp.parentObject._transformComponent.transform.Location);
                parentObject._staticMeshComponent.meshShader.SetVector3("viewPos", Rendering.RenderInterface._activeCamera.parentObject._transformComponent.transform.Location);
                parentObject._staticMeshComponent.meshShader.SetFloat("radius", Rendering.RenderInterface._hardcorelamp._radius);
                //parentObject._staticMeshComponent.meshShader.SetFloat("radius", 320);
        }
    }
}