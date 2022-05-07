using Tyme_Engine.Core;
using OpenTK;
using OpenTK.Input;

namespace Tyme_Engine
{
    class EditorCamera : UserScript
    {
        //private float scrollvalue;
        private MouseState mouse;
        private KeyboardState keyboard;
        private EngineWindow _window;
        Vector2 lastPos;

        public override void Update(float delta)
        {
            //scrollvalue = MathExt.Lerp(scrollvalue, mouse.WheelPrecise, MathExt.Clamp01(delta * 16f));
            //this.parentObject._transformComponent.transform.Location = new Vector3(this.parentObject._transformComponent.transform.Location.X, this.parentObject._transformComponent.transform.Location.Y, scrollvalue/4);
            //parentObject._transformComponent.transform.Rotation = new Vector3(0, parentObject._transformComponent.transform.Rotation.Y + delta * 1f, 0f);
        }
        public EditorCamera(EngineWindow window)
        {
            _window = window;
            //parentObject._transformComponent.transform.Rotation = new Vector3(0, 0, 0);
        }
        public override void PreRender(float delta)
        {
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();
            var transcomp = parentObject._transformComponent;
            var movespeed = delta*4;
            var sensitivity = .1f;

            float deltaX = mouse.X - lastPos.X;
            float deltaY = mouse.Y - lastPos.Y;
            lastPos = new Vector2(mouse.X, mouse.Y);

            if (!mouse.IsButtonDown(MouseButton.Right))
            {
                _window.SetCursorGrabbed(false);
                _window.SetShowMouseCursor(true);
                return;
            }

            _window.SetCursorGrabbed(true);
            _window.SetShowMouseCursor(false);
            transcomp.transform.Rotation += new Vector3(-deltaY, deltaX, 0)*sensitivity;
            transcomp.transform.Rotation.X = MathHelper.Clamp(transcomp.transform.Rotation.X,-89.9f , 89.9f);
            //Debug.Log(transcomp.transform.Rotation.X);

            if (keyboard.IsKeyDown(Key.ShiftLeft))
            {
                movespeed = delta * 12;
            }
            if (keyboard.IsKeyDown(Key.D))
            {
                transcomp.transform.Location += MathExt.GetRightVector(transcomp.transform.Rotation) * movespeed;
            }
            if (keyboard.IsKeyDown(Key.A))
            {
                transcomp.transform.Location += MathExt.GetRightVector(transcomp.transform.Rotation) * -movespeed;
            }
            if (keyboard.IsKeyDown(Key.S))
            {
                transcomp.transform.Location += MathExt.GetForwardVector(transcomp.transform.Rotation)*-movespeed;
            }
            if (keyboard.IsKeyDown(Key.W))
            {
                transcomp.transform.Location += MathExt.GetForwardVector(transcomp.transform.Rotation) * movespeed;
            }
            if (keyboard.IsKeyDown(Key.E))
            {
                transcomp.transform.Location += MathExt.GetUpVector(transcomp.transform.Rotation) * movespeed;
            }
            if (keyboard.IsKeyDown(Key.Q))
            {
                transcomp.transform.Location += MathExt.GetUpVector(transcomp.transform.Rotation) * -movespeed;
            }
        }
    }
}