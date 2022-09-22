using System;

namespace Tyme_Engine.Core
{
    [Serializable]
    public class Component : TObject
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