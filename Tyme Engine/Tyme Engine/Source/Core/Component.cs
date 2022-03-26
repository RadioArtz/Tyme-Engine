using System;

namespace Tyme_Engine.Core
{
    class Component
    {
        public Tyme_Engine.Core.GameObject ParentObject { get; }

        public Component()
        {
        }
        public virtual void OnComponentAttached(Tyme_Engine.Core.GameObject parentObject)
        {

        }

        public virtual void OnComponentDestroyed(EventArgs e)
        {

        }
    }
}