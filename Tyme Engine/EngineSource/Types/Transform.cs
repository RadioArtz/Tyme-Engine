using OpenTK;
using System;
using OpenTK.Mathematics;

namespace Tyme_Engine.Types
{
    [Serializable]
    public struct Transform
    {
        public Vector3 Location;
        public Vector3 Rotation;
        public Vector3 Scale;
    }
}