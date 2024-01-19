using Tyme_Engine.Core;
using OpenTK;
using System.Diagnostics;
using OpenTK.Input;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics;

namespace Tyme_Engine
{
    class ArrayDemoHelper : UserScript   
    {
        public Vector4 DefaultColour = new Vector4(0.0f, 0.5f, 1f, 1f);
        private Vector4 SelectedColour = new Vector4(1, 0, 0, 1);
        private Vector4 SortedColour = new Vector4(0,1,0,1);
        private Vector4 Colour = new Vector4(0.0f, 0.5f, 1f, 1f);
        public int _value { get; private set; }
        public int _position { get; private set; }

        public override void Start()
        {
            UpdateValue(_value);
        }
        public ArrayDemoHelper(int value, int position)
        {
            _value = value;  
            _position = position;
        }
        public override void Update(float delta)
        {

        }
        public override void PreRender(float delta)
        {
            parentObject._staticMeshComponent.meshShader.SetVector4("Color", Colour);
        }
        public void UpdateValue(int value)
        {
            _value = value;
            parentObject._transformComponent.transform.Location = new Vector3(_position * 1.5f, -25, 0);
            parentObject._transformComponent.transform.Scale = new Vector3(1, _value * 1.35f +1, 1);
        }

        public void SetMarkState(int mode)
        {
            switch(mode)
            {
                case 0: Colour = DefaultColour; break;
                case 1: Colour = SelectedColour; break;
                case 2: Colour = SortedColour; break;
            }
        }
    }
}