using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyme_Engine.Core;

namespace Tyme_Engine
{
    static class ObjectManager
    {
        public static List<GameObject> objectBuffer { get; } = new List<GameObject>();

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
        }

        public static List<GameObject> GetAllObjects()
        {
            return objectBuffer;
        }
    }
}
