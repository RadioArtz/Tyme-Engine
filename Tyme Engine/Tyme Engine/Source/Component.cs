using System;

namespace Tyme_Engine.Components
{
    class Component
    {
        public Tyme_Engine.Core.GameObject parentObject { get; }

        public Component()
        {
        }
        public void OnComponentAttached(Tyme_Engine.Core.GameObject parentObject)
        {
        }
    }
}