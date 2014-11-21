using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using Nuclex.Graphics;
using Nuclex.Graphics.Debugging;
using Nuclex.Graphics.Batching;

namespace Nuclex.Fonts.Demo {

  /// <summary>Font rendering demonstration</summary>
  public class FontDemoGame : Microsoft.Xna.Framework.Game {

    /// <summary>Initializes the font demonstration</summary>
    public FontDemoGame() {
      this.Window.AllowUserResizing = true;
      this.IsMouseVisible = true;

      this.content = new ContentManager(Services);
      this.graphics = new GraphicsDeviceManager(this);

      // Create a new camera with a perspective projection matrix
      this.camera = new Camera(
        Matrix.CreateLookAt(
          new Vector3(0.0f, 1.5f, 10.0f), // camera location
          new Vector3(0.0f, 0.0f, 0.0f), // camera focal point
          Vector3.Up // up vector for the camera's orientation
        ),
        Matrix.CreatePerspectiveFieldOfView(
          MathHelper.PiOver4, // field of view
          (float)Window.ClientBounds.Width / (float)Window.ClientBounds.Height, // aspect ratio
          0.01f, 1000.0f // near and far clipping plane
        )
      );
    }

    /// <summary>
    ///   Allows the game to perform any initialization it needs to before starting to run.
    ///   This is where it can query for any required services and load any non-graphic
    ///   related content. Calling base.Initialize will enumerate through any components
    ///   and initialize them as well.
    /// </summary>
    protected override void Initialize() {
      // TODO: Add your initialization logic here

      base.Initialize();
    }

    /// <summary>
    ///   Called when graphics resources need to be loaded. Override this method to load
    ///   any component-specific graphics resources.
    /// </summary>
    protected override void LoadContent() {
      this.arial24sprite = this.content.Load<SpriteFont>("Content/Arial-24-Sprite");
      this.arial24vector = this.content.Load<VectorFont>("Content/Arial-24-Vector");

      this.spriteBatch = new SpriteBatch(this.graphics.GraphicsDevice);
      this.textBatch = new TextBatch(this.graphics.GraphicsDevice);
      this.helloWorldText = this.arial24vector.Fill("Hello World!");
      this.helloWorldText = this.arial24vector.Extrude("Hello World!");
    }

    /// <summary>
    ///   Allows the game to run logic such as updating the world,
    ///   checking for collisions, gathering input and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime) {
      // Allows the default game to exit on Xbox 360 and Windows
      if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        this.Exit();

      // TODO: Add your update logic here
      //this.camera.HandleControls(gameTime);

      base.Update(gameTime);
    }

    /// <summary>This is called when the game should draw itself</summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime) {
      graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

      // ------------------------ #1 XNA SpriteFont class ---------------------- //

      // Draw some traditional text using the XNA SpriteFont class
      this.spriteBatch.Begin();
      this.spriteBatch.DrawString(
        this.arial24sprite,
        "Hello World!",
        new Vector2(60.0f, 60.0f),
        new Color(255, 255, 255, 255)
      );
      this.spriteBatch.End();

      // ----------------------------------------------------------------------- //

      // ----------------------- #2 Nuclex VectorFont class -------------------- //

      Matrix textTransform =
        // 1. Center the text
        Matrix.CreateTranslation(
          -this.helloWorldText.Width / 2.0f, 0.0f, 0.0f
        ) *
        // 2. Increase its extrusion by 2.5 times
        Matrix.CreateScale(1.0f, 1.0f, 2.5f) *
        // 3. Scale it down to 1 world unit in height (font size is 24 units)
        Matrix.CreateScale(1.0f / 24.0f) *
        // 4. Rotate it around the Y axis
        Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds);

      // Now draw some vector text for comparison
      this.textBatch.ViewProjection = this.camera.View * this.camera.Projection;
      this.textBatch.Begin();
      this.textBatch.DrawText(
        this.helloWorldText, textTransform, new Color(255, 255, 255, 255)
      );
      this.textBatch.End();

      // ----------------------------------------------------------------------- //

      base.Draw(gameTime);
    }

    /// <summary>Maintains the graphics device</summary>
    private GraphicsDeviceManager graphics;
    /// <summary>Loads and caches loadable assets</summary>
    private ContentManager content;
    /// <summary>Camera managing the location of the viewer in th scene</summary>
    private Camera camera;
    /// <summary>Sprite batch for text rendering</summary>
    private SpriteBatch spriteBatch;
    /// <summary>Batch for text rendering</summary>
    private TextBatch textBatch;

    /// <summary>Renderable bitmap font imported from Arial.ttf with size 12</summary>
    private SpriteFont arial24sprite;
    /// <summary>Renderable bitmap font imported from Arial.ttf with size 12</summary>
    private VectorFont arial24vector;
    /// <summary>Cached mesh data for the "hello world" vector font text</summary>
    private Text helloWorldText;

  }

} // namespace Nuclex.Fonts.Demo