using System.Collections.Generic;
using System;
using Tyme_Engine.Components;

namespace Tyme_Engine.Core
{
    class GameObject
    {
        public string objectName;

        private List<Component> _childComponents = new List<Component>();
        private StaticMeshComponent _staticMeshComponent;
        private TransformComponent _transformComponent;

        #region Exposed Variables
        public StaticMeshComponent staticMeshComponent { get => _staticMeshComponent; }
        public TransformComponent transformComponent { get => _transformComponent; }
        public List<Component> childComponents { get => _childComponents; }
        #endregion

        public GameObject(string name)
        {
            objectName = name;
            ObjectManager.AddObject(this);
        }

        public void AddComponent(Component componentToAdd)
        {
            childComponents.Add(componentToAdd);
            componentToAdd.OnComponentAttached(this);
            componentToAdd._parentObject = this;

            if (typeof(StaticMeshComponent).IsInstanceOfType(componentToAdd))
                _staticMeshComponent = (StaticMeshComponent)componentToAdd;

            if (typeof(TransformComponent).IsInstanceOfType(componentToAdd))
                _transformComponent = (TransformComponent)componentToAdd;
        }

        public void RemoveComponent(Component componentToRemove)
        {
            componentToRemove.OnComponentDestroyed();
            childComponents.Remove(componentToRemove);
            componentToRemove = null;
        }

        public void RemoveComponent(int indexToRemove)
        {
            childComponents[indexToRemove].OnComponentDestroyed();
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