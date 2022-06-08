using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyme_Engine.Core;
namespace Tyme_Engine.Components
{
    class LightComponentBase : Component
    {
        public OpenTK.Vector4 _lightColor; //RGB for color, Alpha stores Brightness
        public float _radius;
    }
}