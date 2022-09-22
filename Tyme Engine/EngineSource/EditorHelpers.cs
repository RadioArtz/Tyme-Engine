using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyme_Engine.IO;
using Tyme_Engine.Components;
using Tyme_Engine.Core;


namespace Tyme_Engine.Editor
{
    public static class EditorHelpers
    {
        /// <summary>
        /// Tries to Load a Mesh file into the scene
        /// </summary>
        /// <param name="path"> path of the Mesh to load</param>
        /// <returns> Root GameObject of the Mesh Hirarchy</returns>
        public static GameObject CreateMeshFromFile(string path)
        {
            int importIndex = AssetImporter.LoadMeshSyncInt(path);
            if (importIndex == -1)
                return null;

            Assimp.Scene meshScene = (Assimp.Scene)AssetManager.assets[importIndex].Asset_data;
            List<Assimp.Mesh> meshes = meshScene.Meshes;
            Debug.Log("Meshes found in file: "+meshes.Count,ConsoleColor.Yellow);

            if (meshes.Count == 1)
            {
                GameObject _gameObject = new GameObject("Mesh_"+meshScene.RootNode.Name);
                _gameObject.AddComponent(new TransformComponent());
                _gameObject.AddComponent(new StaticMeshComponent(meshes[0],meshScene, Path.GetDirectoryName(path)));
                _gameObject.AddComponent(new TestScript(1));
                return _gameObject;
            }
            else if(meshes.Count >= 1)
            {
                int meshIndexCounter = 0;
                List<GameObject> meshGOs = new List<GameObject>();

                foreach (Assimp.Mesh mesh in meshes)
                {
                    Debug.Log("Creating Mesh " + ("Mesh_" + mesh.Name),ConsoleColor.Cyan);
                    meshGOs.Add(new GameObject("Mesh_" + mesh.Name));
                    meshGOs[meshIndexCounter].AddComponent(new TransformComponent());
                    meshGOs[meshIndexCounter].AddComponent(new StaticMeshComponent(meshes[meshIndexCounter], meshScene, Path.GetDirectoryName(path)));
                    meshGOs[meshIndexCounter].AddComponent(new TestScript(1));
                    meshIndexCounter++;
                }
                return meshGOs[0];
            }
            return null;
        }
    }
}
