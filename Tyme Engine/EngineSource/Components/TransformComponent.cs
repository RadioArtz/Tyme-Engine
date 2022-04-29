using Tyme_Engine.Types;
using Tyme_Engine.Core;
using OpenTK;
using System;

namespace Tyme_Engine.Components
{
    [Serializable]
    class TransformComponent : Component
    {
        public Transform transform;

        public TransformComponent()
        {
            transform.Scale = new Vector3(1, 1, 1);
        }
        public Matrix4 GetModelMatrix()
        {
            Matrix4 model;
            var xRot = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(transform.Rotation.X));
            var yRot = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(transform.Rotation.Y));
            var zRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(transform.Rotation.Z));

            model = Matrix4.Identity;
            model = model * xRot;
            model = model * yRot;
            model = model * zRot;
            model = model * Matrix4.CreateScale(transform.Scale);
            model = model * Matrix4.CreateTranslation(transform.Location);

            return model;
        }
        public Vector3 GetForwardVector()
        {
            Vector3 Target = Vector3.Zero;
            Vector3 camDir = Vector3.Normalize(transform.Location - Target);
            return camDir;
        }
        public Vector3 GetRightVector()
        {
            Vector3 up = Vector3.UnitY;
            Vector3 rightVec = Vector3.Normalize(Vector3.Cross(up,GetForwardVector()));
            return rightVec;
        }
        public Vector3 GetUpVector()
        {
            Vector3 camUp = Vector3.Cross(GetForwardVector(), GetRightVector());
            return camUp;
        }
    }
}