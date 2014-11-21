using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Drawing;

namespace EVALab.Util.Xna.Object
{
    public class TextureLoader
    {

        private Hashtable textures = new Hashtable(10);

        private GraphicsDevice device = null;
        public TextureLoader(GraphicsDevice device)
        {
            this.device = device;
        }

        public Texture2D GetTexture2D(string path)
        {
            Texture2D texture = (Texture2D)textures[path];
            if (texture != null) return texture;

            Bitmap image = (Bitmap)Bitmap.FromFile(path);
            texture = BitmapToTexture2D(device, image);
            textures.Add(path, texture);
            return texture;
        }


        public static Texture2D BitmapToTexture2D(
          GraphicsDevice GraphicsDevice,
          System.Drawing.Bitmap image)
        {
            // Buffer size is size of color array multiplied by 4 because   
            // each pixel has four color bytes  
            int bufferSize = image.Height * image.Width * 4;

            // Create new memory stream and save image to stream so   
            // we don't have to save and read file  
            System.IO.MemoryStream memoryStream =
                new System.IO.MemoryStream(bufferSize);
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

            // Creates a texture from IO.Stream - our memory stream  
            Texture2D texture = Texture2D.FromStream(
                GraphicsDevice, memoryStream);

            return texture;
        }

    }
}
