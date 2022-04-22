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
            view = Matrix4.CreateTranslation(parentObject._transformComponent.transform.Location);
            /*
            view = view * Matrix4.CreateRotationX(parentObject._transformComponent.transform.Rotation.X);
            view = view * Matrix4.CreateRotationY(parentObject._transformComponent.transform.Rotation.Y);
            view = view * Matrix4.CreateRotationZ(parentObject._transformComponent.transform.Rotation.Z);*/
        }
    }
}
