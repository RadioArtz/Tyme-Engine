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

        public static void RemoveScript(UserScript scriptToRemove)
        {
            scriptBuffer.Remove(scriptToRemove);
            
        }

        public static void RemoveScript(int indexToRemove)
        {
            scriptBuffer.RemoveAt(indexToRemove);
            //scriptBuffer[indexToRemove].DestroyObject();
        }

        public static List<UserScript> GetAllScripts()
        {
            return scriptBuffer;
        }
        /*
        public static void DestroyScript(UserScript scriptoDestroy)
        {
            scriptoDestroy = null;
        }*/

        public static void ScriptRender(float delta)
        {
            foreach(UserScript script in scriptBuffer)
            {
                script.PreRender(delta);
            }
        }
        public static void ScriptUpdate(float delta)
        {
            foreach (UserScript script in scriptBuffer)
            {
                script.Update(delta);
            }
        }
    }
}