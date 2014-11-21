#region File Description
//-----------------------------------------------------------------------------
// ModelViewerControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EVALabGC.Tasks.Forms.Xna.Object;
using Microsoft.Xna.Framework.Content;
#endregion

namespace EVALabGC.Tasks.Forms.Xna
{
    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, and displays
    /// a spinning 3D model. The main form class is responsible for loading
    /// the model: this control just displays it.
    /// </summary>
    public class ModelViewerControl : GraphicsDeviceControl
    {

        private SpriteBatch spriteBatch;

        private ContentBuilder contentBuilder = null;

        private ContentManager contentManager = null;

        private TextureLoader textureLoader = null;

        private Color backColor = Color.Black;

        public ContentManager ContentManager
        {
            get
            {
                if (contentManager == null) contentManager = new ContentManager(Services, ContentBuilder.OutputDirectory); 
                return contentManager;
            }
        }

        internal ContentBuilder ContentBuilder 
        {
            get { 
                if (contentBuilder == null) contentBuilder = new ContentBuilder(); 
                return contentBuilder; 
            }
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public TextureLoader TextureLoader
        {
            get {
                if (textureLoader == null) textureLoader = new TextureLoader(this.GraphicsDevice);
                return textureLoader; 
            }
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(this.GraphicsDevice);
            backColor = new Color(Parent.BackColor.R, Parent.BackColor.G, Parent.BackColor.B);
            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            if (drawing) return;
            drawing = true;
            // Clear to the default control background color.
            Color backColor = new Color(BackColor.R, BackColor.G, BackColor.B);
            GraphicsDevice.Clear(backColor);

            spriteBatch.Begin();
            ((XnaForm)this.Parent).DrawActions();
            spriteBatch.End();
            drawing = false;
        }

        private bool drawing = false;

        public bool Drawing
        {
            get { return drawing; }
        }
        
    }
}
