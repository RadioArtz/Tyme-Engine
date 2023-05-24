using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace Tyme_Engine.Rendering
{
    public class Texture
    {
        public int Handle;
        public string m_path;
        public static Texture LoadFromFile(string texturepath, TextureMagFilter filterMode, TextureMinFilter mipmaps,int anisotropicSamples)
        {
            int handle = GL.GenTexture();
            Texture t = new Texture(handle);
            GL.BindTexture(TextureTarget.Texture2D, handle);


            //Clean src string
            texturepath = texturepath.Replace(@"\", "/");

            if (texturepath.EndsWith(".tga"))
            {
                if (!File.Exists(texturepath.Replace(".tga", ".png")))
                {
                    Core.Debug.Log("ERROR could not load .tga file. converting to png...", ConsoleColor.Red);
                    SixLabors.ImageSharp.Image tmpImage = SixLabors.ImageSharp.Image.Load(texturepath);
                    FileStream filestream = new FileStream(texturepath.Replace(".tga", ".png"), FileMode.Create);
                    SixLabors.ImageSharp.Formats.IImageEncoder encoder;
                    tmpImage.Save(filestream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                    filestream.Close();
                }
            }

            Bitmap source = new Bitmap(texturepath.Replace(".tga", ".png"));


            source.RotateFlip(RotateFlipType.RotateNoneFlipY);
            BitmapData data = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.SrgbAlpha, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            source.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)mipmaps);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)filterMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.DetailTextureModeSgis, (int)TextureParameterName.DetailTextureModeSgis);
            GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)All.TextureMaxAnisotropy, anisotropicSamples);
            GL.GenerateTextureMipmap(handle);
            
            source.Dispose();
            return t;
        }

        public static Texture LoadFromMesh(string path, Assimp.Scene assimpScene, int materialIndex, TextureMagFilter filterMode, TextureMinFilter mipmaps, int anisotropicSamples)
        {
            //return (LoadFromFile(@"C:\Users\mathi\Documents\minkra/minicraf-RGBA.png", TextureMagFilter.Nearest, TextureMinFilter.Linear, 0));
            if (assimpScene.Materials[materialIndex].TextureDiffuse.FilePath == null)
                return LoadFromFile(Path.Combine(Environment.CurrentDirectory, "EngineContent/Textures/T_MissingTexture.png"),TextureMagFilter.Nearest,TextureMinFilter.NearestMipmapLinear,16);
            int handle = GL.GenTexture();
            Texture t = new Texture(handle);
            GL.BindTexture(TextureTarget.Texture2D, handle);
            string src = Path.Combine(path, assimpScene.Materials[materialIndex].TextureDiffuse.FilePath);
            
            //Clean src string
            src = src.Replace(@"\", "/");

            if (src.EndsWith(".tga"))
            {
                if(!File.Exists(src.Replace(".tga", ".png")))
                {
                    Core.Debug.Log("ERROR could not load .tga file. converting to png...", ConsoleColor.Red);
                    SixLabors.ImageSharp.Image tmpImage = SixLabors.ImageSharp.Image.Load(src);
                    FileStream filestream = new FileStream(src.Replace(".tga", ".png"), FileMode.Create);
                    SixLabors.ImageSharp.Formats.IImageEncoder encoder;
                    tmpImage.Save(filestream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                    Core.Debug.Log(src,ConsoleColor.Red);
                    filestream.Close();
                } 
            }
            
            Bitmap source = new Bitmap(src.Replace(".tga", ".png"));

            Core.Debug.Log("Loaded Texture from " + src, ConsoleColor.Green);
            source.RotateFlip(RotateFlipType.RotateNoneFlipY);
            
            BitmapData data = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.SrgbAlpha, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            source.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)mipmaps);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)filterMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.DetailTextureModeSgis, (int)TextureParameterName.DetailTextureModeSgis);
            GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)All.TextureMaxAnisotropy, anisotropicSamples);
            
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureParameterName.ClampToEdge);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureParameterName.ClampToEdge);
            GL.GenerateTextureMipmap(handle);

            source.Dispose();
            t.m_path = src;
            return t;
        }

        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public Texture(int glHandle)
        {
            Handle = glHandle;
        }
    }
}