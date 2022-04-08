using Tyme_Engine.Types;
using Tyme_Engine.Core;
using OpenTK;

namespace Tyme_Engine.Components
{
    class TransformComponent : Component
    {
        public Transform transform;
        public Matrix4 model { get; private set; }
        public Matrix4 view { get; private set; }
        public Matrix4 projection { get; private set; }

        public TransformComponent()
        {
            transform.Scale = new Vector3(1, 1, 1);
            // Note that we're translating the scene in the reverse direction of where we want to move.
            view = Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(80.0f), 1.77777f, 0.01f, 100.0f);
        }

        public void UpdateMatrecies()
        {
            var xRot = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(transform.Rotation.X));
            var yRot = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(transform.Rotation.Y));
            var zRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(transform.Rotation.Z));
            model = Matrix4.Identity;
            model = model * xRot;
            model = model * yRot;
            model = model * zRot;
            model = model * Matrix4.CreateScale(transform.Scale);
            model = model * Matrix4.CreateTranslation(transform.Location);
        }
    }
}