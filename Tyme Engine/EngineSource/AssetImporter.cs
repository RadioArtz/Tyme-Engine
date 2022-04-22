﻿using System.Collections.Generic;
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
            return assimpMesh;
        }

        public static float[] ConvertVertecies(Mesh inAssimpMesh,bool b_IncludeTexCoords, int uvChannel)
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