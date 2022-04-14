using OpenTK;
using System;
namespace Tyme_Engine.Types
{
    [Serializable]
    struct Transform
    {
        public Vector3 Location;
        public Vector3 Rotation;
        public Vector3 Scale;
    }
}
