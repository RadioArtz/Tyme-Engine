using Tyme_Engine.Types;
using Tyme_Engine.Core;
using OpenTK;
using System;
using OpenTK.Mathematics;

namespace Tyme_Engine.Components
{
    [Serializable]
    public class TransformComponent : Component
    {
        public Transform transform;

        public TransformComponent()
        {
            transform.Scale = new Vector3(1, 1, 1);
        }
        public Matrix4 GetModelMatrix()
        {
            Matrix4 model;
            model = Matrix4.Identity;
            var xRot = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(transform.Rotation.X));
            var yRot = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(transform.Rotation.Y));
            var zRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(transform.Rotation.Z));

            model *= xRot;
            model *= yRot;
            model *= zRot;
            model *= Matrix4.CreateScale(transform.Scale);
            model *= Matrix4.CreateTranslation(transform.Location);

            return model;
        }
        
    }
}