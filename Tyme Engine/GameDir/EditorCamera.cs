using Tyme_Engine.Core;
using OpenTK;
using OpenTK.Input;
using OpenTK.Mathematics;
using OpenTK.Input.Hid;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Tyme_Engine
{
    class EditorCamera : UserScript
    {
        private MouseState? Mouse;
        private KeyboardState? keyboard;
        private EngineWindow _window;
        Vector2 lastPos;

        public override void Update(float delta)
        {
        }
        public EditorCamera(EngineWindow window)
        {
            _window = window;
        }
        public override void PreRender(float delta)
        {
            var mouse = _window.MouseState;
            var keyboard = _window.KeyboardState;
            var transcomp = parentObject._transformComponent;
            var movespeed = delta*4;
            var sensitivity = .1f;

            float deltaX = mouse.X - lastPos.X;
            float deltaY = mouse.Y - lastPos.Y;
            lastPos = new Vector2(mouse.X, mouse.Y);
            Rendering.RenderInterface._hardcorelamp!._radius = _window.MouseState.Scroll.Y;
            if (!mouse.IsButtonDown(MouseButton.Right))
            {
                _window.SetCursorGrabbed(CursorState.Normal);
                return;
            }
            
            _window.SetCursorGrabbed(CursorState.Grabbed);
            transcomp.transform.Rotation += new Vector3(-deltaY, deltaX, 0)*sensitivity;
            transcomp.transform.Rotation.X = MathHelper.Clamp(transcomp.transform.Rotation.X,-89.9f , 89.9f);

            if (keyboard.IsKeyDown(Keys.LeftShift))
            {
                movespeed = delta * 25;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                transcomp.transform.Location += MathExt.GetRightVector(transcomp.transform.Rotation) * movespeed;
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                transcomp.transform.Location += MathExt.GetRightVector(transcomp.transform.Rotation) * -movespeed;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                transcomp.transform.Location += MathExt.GetForwardVector(transcomp.transform.Rotation)*-movespeed;
            }
            if (keyboard.IsKeyDown(Keys.W))
            {
                transcomp.transform.Location += MathExt.GetForwardVector(transcomp.transform.Rotation) * movespeed;
            }
            if (keyboard.IsKeyDown(Keys.E))
            {
                transcomp.transform.Location += MathExt.GetUpVector(transcomp.transform.Rotation) * movespeed;
            }
            if (keyboard.IsKeyDown(Keys.Q))
            {
                transcomp.transform.Location += MathExt.GetUpVector(transcomp.transform.Rotation) * -movespeed;
            }
            Core.Debug.Log(this.GetParent()._transformComponent.transform.Location);
        }
    }
}