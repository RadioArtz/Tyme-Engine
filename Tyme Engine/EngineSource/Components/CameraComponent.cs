using Tyme_Engine.Core;
using OpenTK;
using OpenTK.Mathematics;

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
            Vector3 rotDegrees = transformref.Rotation;
            //Vector3 rotRadians = (transformref.Rotation * (MathHelper.Pi / 180f));
            view = Matrix4.LookAt(transformref.Location, transformref.Location + MathExt.GetForwardVector(rotDegrees), MathExt.GetUpVector(rotDegrees));
        }
    }
} 