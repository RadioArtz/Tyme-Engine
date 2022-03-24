using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyme_Engine.Rendering;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Tyme_Engine.Components 
{
    class StaticMesh : GameObject
    {
        #region variables
        float[] vertices;

        int VertexBufferObject;
        int VertexArrayObject;
        //int ElementBufferObject;
        Shader shader;
        Texture texture1;
        Texture texture2;
        #endregion //AKA hardcoded verts n shit
        // Note that we're translating the scene in the reverse direction of where we want to move.
        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), 1280 / 720, 0.1f, 100.0f);
        Assimp.Mesh assmesh;

        public StaticMesh(Assimp.Mesh assimpMesh, bool bShouldRender, bool bShouldShadow)
        {
            assmesh = assimpMesh;
            vertices = Tyme_Engine.IO.AssetImporter.convertVertecies(assimpMesh).ToArray();
            #region VBO und so machen sachen, texture idk hardcoded zum teil sowieso
            // initialize vertexBufferObject
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            //initialize VertexArrayObject
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            /*
            //initialize ElementBufferObject
            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            */
            shader = new Shader("EngineContent/Shaders/shader.vert", "EngineContent/Shaders/shader.frag");
            shader.Use();

            //send VertCoords
            GL.VertexAttribPointer(shader.GetAttribLocation("aPos"), 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //send texCoords
            GL.VertexAttribPointer(shader.GetAttribLocation("aTexCoord"), 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            texture1 = Texture.LoadFromFile("EngineContent/container.jpg");
            texture1.Use(TextureUnit.Texture0);

            texture2 = Texture.LoadFromFile("EngineContent/awesomeface.png");
            texture2.Use(TextureUnit.Texture1);

            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);
            #endregion
            
        }

        internal void RenderMesh(OpenTK.Matrix4 view)
        {
            GL.BindVertexArray(VertexArrayObject);
            texture1.Use(TextureUnit.Texture0);
            texture2.Use(TextureUnit.Texture1);
            shader.Use();
            shader.SetMatrix4("transform", createTransformMatrix());
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);
            //GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, (vertices.Length / 3));
            //GL.DrawElements(PrimitiveType.Triangles, vertices.Length / 3,DrawElementsType.UnsignedInt, assmesh.GetIndices());
        }
    }
}