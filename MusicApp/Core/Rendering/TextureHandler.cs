using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Core.Rendering
{
    public static class TextureHandler
    {
        private static List<int> _texturesLoaded = new List<int>();

        public static uint LoadTexture(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("There is no file");
            }
            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);
            Bitmap bmp = new Bitmap(file);
            BitmapData data = new BitmapData();
            data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            FileHelper.ImageExtension ex = FileHelper.GetImageExtension(file);

            switch (ex) {
                case FileHelper.ImageExtension.JPG:
                    {
                        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0,
              OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, data.Scan0);
                        break;
                    }
                case FileHelper.ImageExtension.PNG:
                    {
                        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0,
              OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                        break;
                    }
                default:
                    {
                        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0,
                 OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, data.Scan0);
                        break;
                    }
            }
            bmp.UnlockBits(data);
            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            bmp.Dispose();
            _texturesLoaded.Add(id);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            return (uint)id;

        }

        public static void FreeAllTextures()
        {
            foreach (var item in _texturesLoaded)
            {
                GL.DeleteTexture(item);
            }
        }

        public static void UseTexture2D(TextureUnit unit,uint textureID)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, textureID);
        }
    }
}
