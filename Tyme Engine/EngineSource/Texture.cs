using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace Tyme_Engine.Rendering
{
    public class Texture
    {
        public int Handle;

        public static Texture LoadFromFile(string texturepath, TextureMagFilter filterMode, TextureMinFilter mipmaps,int anisotropicSamples)
        {
            int handle = GL.GenTexture();
            Texture t = new Texture(handle);
            GL.BindTexture(TextureTarget.Texture2D, handle);
            Bitmap source = new Bitmap(texturepath);
            source.RotateFlip(RotateFlipType.RotateNoneFlipY);
            BitmapData data = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            source.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)mipmaps);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)filterMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.DetailTextureModeSgis, (int)TextureParameterName.DetailTextureModeSgis);
            GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)All.TextureMaxAnisotropy, anisotropicSamples);
            GL.GenerateTextureMipmap(handle);
            
            source.Dispose();

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