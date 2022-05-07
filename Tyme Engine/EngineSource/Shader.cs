using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

//fair warning, i have no clue wtf all of this does anymore but im gonna rewrite at some point so i actually do lol
namespace Tyme_Engine.Rendering
{
    public class Shader
    {
        public int Handle { get; private set; }
        private readonly Dictionary<string, int> uniformLocations;
        private bool disposedValue = false;

        public Shader(string vertexPath, string fragmentPath)
        {
            //define shader paths
            string VertexShaderSource;
            string FragmentShaderSource;

            //create shaders and define Type
            int VertexShader = GL.CreateShader(ShaderType.VertexShader);
            int FragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            //load shadercode and store in variable
            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                VertexShaderSource = reader.ReadToEnd();
            }
            using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                FragmentShaderSource = reader.ReadToEnd();
            }

            //bind shadersourcecode
            GL.ShaderSource(VertexShader, VertexShaderSource);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);

            //Compile shaders
            CompileShader(VertexShader);

            string infoLogVert = GL.GetShaderInfoLog(VertexShader);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);

            CompileShader(FragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(FragmentShader);

            if (infoLogFrag != System.String.Empty)
                System.Console.WriteLine(infoLogFrag);

            //Link Shaders
            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.LinkProgram(Handle);

            //Cleanup
            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);

            // Additional useful shit
            // First, we have to get the number of active uniforms in the shader.
            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);
            // Next, allocate the dictionary to hold the locations.
            uniformLocations = new Dictionary<string, int>();
            Core.Debug.Log("List all uniforms in shader");
            // Loop over all the uniforms,
            for (var i = 0; i < numberOfUniforms; i++)
            {
                // get the name of this uniform,
                var key = GL.GetActiveUniform(Handle, i, out _, out _);

                // get the location,
                var location = GL.GetUniformLocation(Handle, key);

                // and then add it to the dictionary.
                uniformLocations.Add(key, location);
                Core.Debug.Log((key, location));
            }
        }

        private static void CompileShader(int shader)
        {
            // Try to compile the shader
            GL.CompileShader(shader);

            // Check for compilation errors
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                // We can use `GL.GetShaderInfoLog(shader)` to get information about the error.
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        //final cleanup
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if(Handle!=-1)
                GL.DeleteProgram(Handle);
                Handle = -1;
                disposedValue = true;
            }
        }

        ~Shader()
        {
            if(Handle!=-1)
            GL.DeleteProgram(Handle);
            Handle = -1;
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region setting utils
        // Uniform setters
        // Uniforms are variables that can be set by user code, instead of reading them from the VBO.
        // You use VBOs for vertex-related data, and uniforms for almost everything else.

        // Setting a uniform is almost always the exact same, so I'll explain it here once, instead of in every method:
        //     1. Bind the program you want to set the uniform on
        //     2. Get a handle to the location of the uniform with GL.GetUniformLocation.
        //     3. Use the appropriate GL.Uniform* function to set the uniform.

        /// <summary>
        /// Set a uniform int on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetInt(string name, int data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(uniformLocations[name], data);
        }

        /// <summary>
        /// Set a uniform float on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetFloat(string name, float data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(uniformLocations[name], data);
        }

        /// <summary>
        /// Set a uniform Matrix4 on this shader
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        /// <remarks>
        ///   <para>
        ///   The matrix is transposed before being sent to the shader.
        ///   </para>
        /// </remarks>
        public void SetMatrix4(string name, OpenTK.Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(uniformLocations[name], true, ref data);
        }

        /// <summary>
        /// Set a uniform Vector3 on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetVector3(string name, OpenTK.Vector3 data)
        {
            //if (uniformLocations.ContainsKey(name))
            //{
            if (uniformLocations.ContainsKey(name))
            {
                GL.UseProgram(Handle);
                GL.Uniform3(uniformLocations[name], data);
            }
            //}
            //else
            //{ 
            //foreach(string str in uniformLocations.Keys)
            //    {
            //        Core.Debug.Log(str);
            //    }
            //}
            
        }

        public void SetVector4(string name, OpenTK.Vector4 data)
        {
            if (uniformLocations.ContainsKey(name))
            {
                GL.UseProgram(Handle);
                GL.Uniform4(uniformLocations[name], data);
            }
        }
        #endregion
    }
}
