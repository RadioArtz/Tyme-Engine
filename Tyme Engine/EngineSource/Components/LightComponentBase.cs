using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyme_Engine.Core;
using OpenTK.Mathematics;

namespace Tyme_Engine.Components
{
    class LightComponentBase : Component
    {
        public Vector4 _lightColor = new Vector4(0,0,1,1); //RGB for color, Alpha stores Brightness
        
        public float _radius;
    }
}