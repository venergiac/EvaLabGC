using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Drawing;
using System.Collections;
using System.IO;
using System.Diagnostics;

namespace EVALabGC.Tasks.Forms.Xna
{
    public class Action : ROI.Action
    {

        protected Texture2D texture = null;
        protected Vector2 objectCenter = Vector2.Zero;
        protected Vector2 objectPosition = Vector2.Zero;
        protected ModelViewerControl controller = null;

        public Action(ModelViewerControl controller, int id, string action, string[] actionParameters, bool target)
        {
            this.controller = controller;
            DoParse(id, action, actionParameters, target);
            objectPosition = new Vector2(targetX, targetY);
        }

        protected override void MakeTexture(string imagePath)
        {
            texture = controller.TextureLoader.GetTexture2D(imagePath);
        }

        protected override void DrawTexture(Graphics myGraphics)
        {
            objectPosition.X  = targetX;
            objectPosition.Y =  targetY;
            
            if (controller.Drawing)
            {
                controller.SpriteBatch.Draw(texture, objectPosition, null, Microsoft.Xna.Framework.Color.White, 0.0f, objectCenter, 1.0f, SpriteEffects.None, 0);
            }
        }

    }
}

