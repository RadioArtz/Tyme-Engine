using System;
using OpenTK;

namespace Tyme_Engine
{
    class GameObject
    {
        float time;
        #region Transform
        public Vector3 Position = new Vector3(0, 0, 0);
        public Vector3 Rotation = new Vector3(0, 0, 0);
        public Vector3 Scale = new Vector3(1, 1, 1);
        #endregion

        public GameObject()
        {
        }

        public Matrix4 createTransformMatrix()
        {
            var trans = Matrix4.Identity;
            trans = trans * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation.Z));
            trans = trans * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Rotation.X));
            trans = trans * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Rotation.Y));
            trans = trans * Matrix4.CreateScale(Scale);
            trans = trans * Matrix4.CreateTranslation(Position);
            return trans;
        }
    }
}