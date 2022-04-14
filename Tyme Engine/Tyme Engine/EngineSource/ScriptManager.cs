using System.Collections.Generic;
using Tyme_Engine.Core;

namespace Tyme_Engine
{
    static class ScriptManager
    {
        public static List<UserScript> scriptBuffer { get; private set; } = new List<UserScript>();

        public static void AddScript(UserScript scriptToAdd)
        {
            scriptBuffer.Add(scriptToAdd);
            scriptToAdd.Start();
        }

        public static void RemoveObject(UserScript scriptToRemove)
        {
            scriptBuffer.Remove(scriptToRemove);
            
        }

        public static void RemoveObject(int indexToRemove)
        {
            scriptBuffer.RemoveAt(indexToRemove);
            //scriptBuffer[indexToRemove].DestroyObject();
        }

        public static List<UserScript> GetAllScripts()
        {
            return scriptBuffer;
        }

        public static void DestroyObject(GameObject objectToDestroy)
        {
            objectToDestroy = null;
        }
        
        public static void ScriptUpdate(float delta)
        {
            foreach(UserScript script in scriptBuffer)
            {
                script.Update(delta);
            }
        }
        public static void ScriptFixedUpdate(float delta)
        {
            foreach(UserScript script in scriptBuffer)
            {
                script.FixedUpdate(delta);
            }
        }
    }
}