using System.Collections.Generic;
using Tyme_Engine.Core;

namespace Tyme_Engine
{
    static class ObjectManager
    {
        public static List<GameObject> objectBuffer { get; private set; } = new List<GameObject>();

        public static void AddObject(GameObject objectToAdd)
        {
            objectBuffer.Add(objectToAdd);
        }

        public static void RemoveObject(GameObject objectToRemove)
        {
            objectBuffer.Remove(objectToRemove);
            
        }

        public static void RemoveObject(int indexToRemove)
        {
            objectBuffer.RemoveAt(indexToRemove);
            objectBuffer[indexToRemove].DestroyObject();
        }

        public static List<GameObject> GetAllObjects()
        {
            return objectBuffer;
        }

        public static void DestroyObject(GameObject objectToDestroy)
        {
            objectToDestroy = null;
        }
    }
}