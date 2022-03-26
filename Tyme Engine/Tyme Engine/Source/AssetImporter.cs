using System.Collections.Generic;
using System.Linq;
using Assimp;

namespace Tyme_Engine.IO
{
    static class AssetImporter

    {
        public static Mesh LoadMeshSync(string path)
        {
            var assimpContext = new AssimpContext();
            var assimpScene = assimpContext.ImportFile(path/*, PostProcessSteps.GenerateNormals | PostProcessSteps.GenerateUVCoords | PostProcessSteps.Triangulate*/);
            var assimpMesh = assimpScene.Meshes.First();
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
    }
}