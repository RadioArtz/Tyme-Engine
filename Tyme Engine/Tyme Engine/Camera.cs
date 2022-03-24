using OpenTK;
using OpenTK.Input;
using System;

namespace Tyme_Engine
{
    class Camera : GameObject
    {
        public Vector3 UpVector = Vector3.UnitY;
        public Vector3 RightVector = Vector3.UnitX;
        public Vector3 ForwardVector = Vector3.UnitZ;
        Vector2 lastPos;
        public float speed = 2.5f;
        float sensitivity = 0.1f;

        public Matrix4 view = Matrix4.Identity;

        //todo: make this shit cleaner and transfer more code from Engine.cs over here and make the Camera have more control over Rendering. also make it be less shit

        public Camera()
        {
        }

        public void UpdateVectors()
        {
            Vector3 fwdVec;

            fwdVec.X = (float)Math.Cos(MathHelper.DegreesToRadians(Rotation.X)) * (float)Math.Cos(MathHelper.DegreesToRadians(Rotation.Y));
            fwdVec.Y = (float)Math.Sin(MathHelper.DegreesToRadians(Rotation.X));
            fwdVec.Z = (float)Math.Cos(MathHelper.DegreesToRadians(Rotation.X)) * (float)Math.Sin(MathHelper.DegreesToRadians(Rotation.Y));
            fwdVec.Normalize();

            Vector3 cameraRight;
            cameraRight = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, fwdVec));

            Vector3 cameraUp = Vector3.Cross(fwdVec, cameraRight);

            view = Matrix4.LookAt(Position, Position + ForwardVector, UpVector);

            UpVector = cameraUp;
            RightVector = cameraRight*-1;
            ForwardVector = fwdVec*-1; 
        }

        public void DoInput()
        {
            MouseState mouse = Mouse.GetState();

            float deltaX = mouse.X - lastPos.X;
            float deltaY = mouse.Y - lastPos.Y;

            lastPos = new Vector2(mouse.X, mouse.Y);

            Rotation.Y += deltaX * sensitivity;
            Rotation.X += deltaY * sensitivity;

            if (Rotation.X > 89.0f)
            {
                Rotation.X = 89.0f;
            }
            else if (Rotation.X < -89.0f)
            {
                Rotation.X = -89.0f;
            }
        }
    }
}
