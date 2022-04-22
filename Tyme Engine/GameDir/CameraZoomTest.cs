using Tyme_Engine.Core;
using OpenTK;

namespace Tyme_Engine
{
    class CameraZoomTest : UserScript
    {
        private float scrollvalue;

        public override void Update(float delta)
        {
            OpenTK.Input.MouseState scroll = OpenTK.Input.Mouse.GetState();
            scrollvalue = MathExt.Lerp(scrollvalue, scroll.WheelPrecise, MathExt.Clamp01(delta * 16f));
            this.parentObject._transformComponent.transform.Location = new Vector3(0, 0, scrollvalue);
        }
    }
}