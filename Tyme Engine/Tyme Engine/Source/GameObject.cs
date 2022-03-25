using System.Collections.Generic;
using Tyme_Engine.Components;

namespace Tyme_Engine.Core
{
    class GameObject
    {
        private List<Component> childComponents = new List<Component>();
        public string objectName;
        public GameObject(string name)
        {
            objectName = name;
        }

        public void AddComponent(Component componentToAdd)
        {
            childComponents.Add(componentToAdd);
            componentToAdd.OnComponentAttached(this);
        }

        public void RemoveComponent(Component componentToRemove)
        {
            childComponents.Remove(componentToRemove);
        }

        public void RemoveComponent(int indexToRemove)
        {
            childComponents.RemoveAt(indexToRemove);
        }
    }
}
