using Tyme_Engine.Core;
using OpenTK;
using System.Diagnostics;
using OpenTK.Input;
using System;
using OpenTK.Mathematics;

namespace Tyme_Engine
{
    class Hoverlamp : UserScript   
    {
        private Stopwatch _timer = new Stopwatch();
        
        public override void Start()
        {
            _timer.Start();
        }
        public Hoverlamp(float direction)
        {   
        }
        public override void Update(float delta)
        {

        }
        public override void PreRender(float delta)
        {
            this.parentObject._transformComponent.transform.Location = new Vector3(0, (float)Math.Sin(_timer.ElapsedMilliseconds/256f)*16f+64f, 0);
            this.parentObject._transformComponent.transform.Scale = new Vector3(1, 1, 1);
        }
    }
}