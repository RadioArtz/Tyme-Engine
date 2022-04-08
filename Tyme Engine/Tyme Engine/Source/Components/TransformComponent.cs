using Tyme_Engine.Types;
using Tyme_Engine.Core;
using OpenTK;

namespace Tyme_Engine.Components
{
    class TransformComponent : Component
    {
        public Transform componentTransform;
        public  Matrix4 model;
        public readonly Matrix4 view;
        public readonly Matrix4 projection;
            
        public TransformComponent()
        {/*
            model = Matrix4.CreateRotationY(45);
            model *= Matrix4.CreateRotationX(35);*/
            model = Matrix4.Identity;
            // Note that we're translating the scene in the reverse direction of where we want to move.
            view = Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(80.0f), 800/800, 0.1f, 100.0f);
        }

        public void UpdateMatrecies()
        {
            var xRot = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(componentTransform.Rotation.X));
            var yRot = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(componentTransform.Rotation.Y));
            var zRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(componentTransform.Rotation.Z));
            model = Matrix4.Identity;
            model = model * xRot;
            model = model * yRot;
            model = model * zRot;
            /*
            model = model * Matrix4.CreateScale(componentTransform.Scale);
            model = model * Matrix4.CreateTranslation(componentTransform.Location);
            */
        }
    }
}