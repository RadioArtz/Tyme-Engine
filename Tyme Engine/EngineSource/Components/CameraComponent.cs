using Tyme_Engine.Core;
using OpenTK;

namespace Tyme_Engine.Components
{
    class CameraComponent : Component
    {
        public Matrix4 view { get; private set; }
        public CameraComponent()
        {
            Rendering.RenderInterface.AddCamera(this);
        }

        public void UpdateViewMatrix()
        {
            var transformref = parentObject._transformComponent.transform;
            //view = Matrix4.CreateTranslation(transformref.transform.Location);
            view = Matrix4.LookAt(transformref.Location, transformref.Location + MathExt.GetForwardVector(transformref.Rotation), MathExt.GetUpVector(transformref.Rotation));
                /*
            view = view * Matrix4.CreateRotationX(parentObject._transformComponent.transform.Rotation.X);
            view = view * Matrix4.CreateRotationY(parentObject._transformComponent.transform.Rotation.Y);
            view = view * Matrix4.CreateRotationZ(parentObject._transformComponent.transform.Rotation.Z);*/
        }
    }
}
