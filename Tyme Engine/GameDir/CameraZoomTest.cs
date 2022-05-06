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
            //parentObject._transformComponent.transform.Rotation = new Vector3(0, parentObject._transformComponent.transform.Rotation.Y + delta * 1f, 0f);
        }
        public CameraZoomTest()
        {
            //parentObject._transformComponent.transform.Rotation = new Vector3(0, 0, 0);
        }
        public override void PreRender(float delta)
        {
            var transcomp = parentObject._transformComponent;
            var test = 0.1f;
            KeyboardState input = Keyboard.GetState();
            Debug.Log(transcomp.transform.Location);
            if (input.IsKeyDown(Key.D))
            {
                transcomp.transform.Location += MathExt.GetRightVector(transcomp.transform.Rotation) * test;
            }
            if (input.IsKeyDown(Key.A))
            {
                transcomp.transform.Location += MathExt.GetRightVector(transcomp.transform.Rotation) * -test;
            }
            if (input.IsKeyDown(Key.S))
            {
                transcomp.transform.Location += MathExt.GetForwardVector(transcomp.transform.Rotation)*-test;
            }
            if (input.IsKeyDown(Key.W))
            {
                transcomp.transform.Location += MathExt.GetForwardVector(transcomp.transform.Rotation) * test;
            }
            if (input.IsKeyDown(Key.Q))
            {
                transcomp.transform.Location += MathExt.GetUpVector(transcomp.transform.Rotation) * test;
            }
            if (input.IsKeyDown(Key.E))
            {
                transcomp.transform.Location += MathExt.GetUpVector(transcomp.transform.Rotation) * -test;
            }
            if (input.IsKeyDown(Key.Right))
            {
                transcomp.transform.Rotation += new Vector3(0, delta * 1f, 0);
            }
            if (input.IsKeyDown(Key.Left))
            {
                transcomp.transform.Rotation += new Vector3(0, delta * -1f, 0);
            }
        }
    }
}