using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyme_Engine.Core;
using OpenTK.Mathematics;

namespace Tyme_Engine.Components
{
    class PointLightComponent : LightComponentBase
    {
        public PointLightComponent()
        {
            _lightColor = new Vector4(1, 1, 1, 1);
            _radius = 16f;
        }
    }
}
