using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Microsoft.VisualBasic;
using System.Windows.Forms;

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
            Debug.Log(gameObjects.Length);
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
        public void OpenScene() { DeSerializeScene(); }
        private void DeSerializeScene()
        {
            string input = Interaction.InputBox("Open Scene File", "Enter Scene Name", "New Scene");
            sceneName = input;

            if(File.Exists(Directory.GetCurrentDirectory() + "/GameDir/" + sceneName + ".scn"))
            {
                using (FileStream fs = File.OpenRead(Directory.GetCurrentDirectory() + "/GameDir/" + sceneName + ".scn"))
                {
                    BinaryFormatter b = new BinaryFormatter();
                    b.Deserialize(fs);
                    
                    fs.Close();
                    OnDeserialized();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Could not find scene file.","Scene Open Error",MessageBoxButtons.OK);
                DeSerializeScene();
            }
        }

        private void OnDeserialized()
        {
            foreach(GameObject go in gameObjects)
            {
                GameObject tmpobj = new GameObject(go.objectName);
                foreach (Component comp in go.childComponents)
                {
                    tmpobj.AddComponent(comp);
                }
            }
            Debug.Log(gameObjects == null);
            Debug.Log(sceneName);
        }
    }
}