using Tyme_Engine.Core;
using OpenTK;
using OpenTK.Input;
namespace Tyme_Engine
{
    class CameraZoomTest : UserScript
    {
        private float scrollvalue;

        public override void Update(float delta)
        {
            OpenTK.Input.MouseState scroll = OpenTK.Input.Mouse.GetState();
            OpenTK.Input.KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            scrollvalue = MathExt.Lerp(scrollvalue, scroll.WheelPrecise, MathExt.Clamp01(delta * 16f));
            //this.parentObject._transformComponent.transform.Location = new Vector3(this.parentObject._transformComponent.transform.Location.X, this.parentObject._transformComponent.transform.Location.Y, scrollvalue/4);
        }

        public override void PreRender(float delta)
        {
            KeyboardState input = Keyboard.GetState();
            if (input.IsKeyDown(Key.D))
            {
                parentObject._transformComponent.transform.Location += new Vector3(-delta, 0, 0);
            }
            if (input.IsKeyDown(Key.A))
            {
                parentObject._transformComponent.transform.Location += new Vector3(delta, 0, 0);
            }
            if (input.IsKeyDown(Key.S))
            {
                parentObject._transformComponent.transform.Location += new Vector3(0, 0, -delta);
            }
            if (input.IsKeyDown(Key.W))
            {
                parentObject._transformComponent.transform.Location += new Vector3(0, 0, delta);
            }
            if (input.IsKeyDown(Key.Q))
            {
                parentObject._transformComponent.transform.Location += new Vector3(0, delta, 0);
            }
            if (input.IsKeyDown(Key.E))
            {
                parentObject._transformComponent.transform.Location += new Vector3(0, -delta, 0);
            }
        }
    }
}