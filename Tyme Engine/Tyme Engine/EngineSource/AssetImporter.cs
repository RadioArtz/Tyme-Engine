using System.Collections.Generic;
using System.Linq;
using Assimp;
using Tyme_Engine.Core;
namespace Tyme_Engine.IO
{
    static class AssetImporter

    {
        public static Mesh LoadMeshSync(string path)
        {
            var assimpContext = new AssimpContext();
            var assimpScene = assimpContext.ImportFile(path, PostProcessSteps.GenerateNormals | PostProcessSteps.GenerateUVCoords | PostProcessSteps.Triangulate);
            var assimpMesh = assimpScene.Meshes.First();
            Debug.Log(assimpScene.MeshCount);
            return assimpMesh;
        }

        public static List<float> ConvertVertecies(Mesh inAssimpMesh)
        {
            var tmplist = new List<float>();
            foreach(Vector3D v3d in inAssimpMesh.Vertices)
            {
                tmplist.Add(v3d.X);
                tmplist.Add(v3d.Y);
                tmplist.Add(v3d.Z);
            }
            return tmplist;
        }

        public static int[] ConvertIndecies(Mesh inAssimpMesh)
        {
            var tmplist = new List<int>();
            foreach (Face face in inAssimpMesh.Faces)
            {
                foreach (int index in face.Indices)
                {
                    tmplist.Add(index);
                }
            }
            return tmplist.ToArray();
        }
    }
}