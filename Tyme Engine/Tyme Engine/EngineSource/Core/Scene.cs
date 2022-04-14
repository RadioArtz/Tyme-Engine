using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Microsoft.VisualBasic;

namespace Tyme_Engine.Core
{
    [Serializable]
    class Scene
    {
        
        private GameObject[] gameObjects; //covert from list in object manager when saving
        public string sceneName { private set; get; }

        public void SaveScene(){ SerializeScene(); }

        private void SerializeScene()
        {
            Debug.Log("Enter SceneName: ");
            string input = Interaction.InputBox("Enter Scene name", "Save Scene As", "New Scene");
            //sceneName = Console.ReadLine();
            sceneName = input;
            Array.Resize(ref gameObjects, ObjectManager.objectBuffer.Count);
            gameObjects = ObjectManager.objectBuffer.ToArray();
            if(!Directory.Exists(Directory.GetCurrentDirectory() + "/GameDir"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/GameDir");

            Debug.Log("Saving scene to" + Directory.GetCurrentDirectory() + "/GameDir/" + sceneName + ".scn");
            using (FileStream fs = File.Create(Directory.GetCurrentDirectory() + "/GameDir/" + sceneName + ".scn"))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(fs, this);
                fs.Close();
            }
        }
    }
}
