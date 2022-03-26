using System.Collections.Generic;
using System;
namespace Tyme_Engine.Core
{
    class GameObject
    {
        private List<Component> childComponents = new List<Component>();
        public string objectName;
        public GameObject(string name)
        {
            objectName = name;
            ObjectManager.AddObject(this);
        }

        public void AddComponent(Component componentToAdd)
        {
            childComponents.Add(componentToAdd);
            componentToAdd.OnComponentAttached(this);
        }

        public void RemoveComponent(Component componentToRemove)
        {
            childComponents.Remove(componentToRemove);
            componentToRemove = null;
        }

        public void RemoveComponent(int indexToRemove)
        {
            childComponents[indexToRemove] = null;
            childComponents.RemoveAt(indexToRemove);
        }

        public void DestroyObject()
        {
            ObjectManager.DestroyObject(this);
        }

        public List<Component> GetComponents()
        {
            return childComponents;
        }

    }
}
