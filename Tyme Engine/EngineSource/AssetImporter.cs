using System.Collections.Generic;
using Assimp;
using System;

namespace Tyme_Engine.IO
{
    static class AssetImporter

    {
        public static Assimp.Scene LoadMeshSync(string path)
        {
            var assimpContext = new AssimpContext();
            var assimpScene = assimpContext.ImportFile(path,PostProcessSteps.GenerateNormals |  PostProcessSteps.GenerateUVCoords | PostProcessSteps.Triangulate | PostProcessSteps.FindInvalidData |PostProcessSteps.OptimizeMeshes | PostProcessSteps.ImproveCacheLocality | PostProcessSteps.JoinIdenticalVertices);
            int test = AssetManager.RegisterAsset(assimpScene, assimpScene.RootNode.Name, AssetManager.AssetType.Mesh);
            return (Assimp.Scene)AssetManager.assets[test].Asset_data;
           
        }
        public static int LoadMeshSyncInt(string path)
        {
            Core.Debug.Log("Loading Mesh from " + path, ConsoleColor.Black, ConsoleColor.Gray);
            var assimpContext = new AssimpContext();
            var assimpScene = assimpContext.ImportFile(path,PostProcessSteps.GenerateNormals | PostProcessSteps.GenerateUVCoords | PostProcessSteps.Triangulate | PostProcessSteps.FindInvalidData | PostProcessSteps.OptimizeMeshes | PostProcessSteps.ImproveCacheLocality | PostProcessSteps.JoinIdenticalVertices);
            int test = AssetManager.RegisterAsset(assimpScene, assimpScene.RootNode.Name, AssetManager.AssetType.Mesh);
            return test;
        }

        public static float[] ConvertVertecies(Mesh inAssimpMesh,bool b_IncludeTexCoords, bool b_IncludeNormals, int uvChannel)
        {
            int index = 0;
            var tmplist = new List<float>();
            foreach(Vector3D v3d in inAssimpMesh.Vertices)
            {
                tmplist.Add(v3d.X);
                tmplist.Add(v3d.Y);
                tmplist.Add(v3d.Z);

                if (b_IncludeTexCoords)
                {
                    tmplist.Add((inAssimpMesh.TextureCoordinateChannels[uvChannel])[index].X);
                    tmplist.Add((inAssimpMesh.TextureCoordinateChannels[uvChannel])[index].Y);
                }
                if (b_IncludeNormals)
                {
                    tmplist.Add(inAssimpMesh.Normals[index].X);
                    tmplist.Add(inAssimpMesh.Normals[index].Y);
                    tmplist.Add(inAssimpMesh.Normals[index].Z);
                }
                index++;
            }
            return tmplist.ToArray();
        }

        public static float[] ConvertUVCoords(Mesh inAssimpMesh, int uvChannel)
        {
            var tmplist = new List<float>();
            foreach(Assimp.Vector3D v3d in inAssimpMesh.TextureCoordinateChannels[uvChannel])
            {
                tmplist.Add(v3d.X);
                tmplist.Add(v3d.Y);
            }
            return tmplist.ToArray();
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