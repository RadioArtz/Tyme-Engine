using System.Collections.Generic;
using System;
using Tyme_Engine.Components;

namespace Tyme_Engine.Core
{
    [Serializable]
    class GameObject
    {
        public string objectName;

        public List<Component> childComponents { get; private set; } = new List<Component>();
        public StaticMeshComponent _staticMeshComponent { get; private set; }
        public TransformComponent _transformComponent { get; private set; }

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

            if (typeof(UserScript).IsInstanceOfType(componentToAdd))
                ScriptManager.AddScript((UserScript)componentToAdd);

            if (typeof(PointLampComponent).IsInstanceOfType(componentToAdd))
                Rendering.RenderInterface.hardcorelamp = (PointLampComponent)componentToAdd;
        }

        public void RemoveComponent(Component componentToRemove)
        {
            componentToRemove.OnComponentDestroyed();
            childComponents.Remove(componentToRemove);
            //componentToRemove = null;
        }

        public void RemoveComponent(int indexToRemove)
        {
            childComponents[indexToRemove].OnComponentDestroyed();
            childComponents[indexToRemove] = null;
            childComponents.RemoveAt(indexToRemove);
        }

        public void DestroyObject()
        {
            _staticMeshComponent?.OnComponentDestroyed();
            ObjectManager.DestroyObject(this);
        }

        public List<Component> GetComponents()
        {
            return childComponents;
        }
    }
}