using System.Collections.Generic;
using Tyme_Engine.Core;

namespace Tyme_Engine
{
    static class ObjectManager
    {
        public static List<GameObject> GameObjects { get;  set; } = new List<GameObject>();

        public static void AddObject(GameObject objectToAdd)
        {
            GameObjects.Add(objectToAdd);
        }

        public static void RemoveObject(GameObject objectToRemove)
        {
            GameObjects.Remove(objectToRemove);
        }

        public static void RemoveObject(int indexToRemove)
        {
            GameObjects.RemoveAt(indexToRemove);
            GameObjects[indexToRemove].DestroyObject();
        }

        public static List<GameObject> GetAllObjects()
        {
            return GameObjects;
        }

        public static void DestroyObject(GameObject objectToDestroy)
        {
            //objectToDestroy = null;
        }
    }
}