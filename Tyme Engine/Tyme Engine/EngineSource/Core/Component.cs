using System;
using Tyme_Engine.Core;

namespace Tyme_Engine.Core
{
    [Serializable]
    class Component
    {
        internal GameObject _parentObject;
        public GameObject parentObject { get => _parentObject; }

        public Component()
        {
        }
        public virtual void OnComponentAttached(GameObject parentObject)
        {

        }

        public virtual void OnComponentDestroyed()
        {

        }

        public GameObject GetParent()
        {
            return parentObject;
        }
    }
}