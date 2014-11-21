using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Nuclex.Graphics.Batching;

namespace Nuclex.Graphics.Demo {

  /// <summary>
  /// This is the main type for your game
  /// </summary>
  public class GraphicsDemoGame : Microsoft.Xna.Framework.Game {

    /// <summary>Initializes a new graphics demo game</summary>
    public GraphicsDemoGame() {
      this.graphics = new GraphicsDeviceManager(this);

      IsMouseVisible = true;

      Content.RootDirectory = "Content";
      
      this.tileVertices = new VertexPositionColor[6];
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize() {
#if XNA_4
      this.primitiveBatch = new PrimitiveBatch<VertexPositionColor>(GraphicsDevice);
        
#else
      this.vertexDeclaration = new VertexDeclaration(
        this.graphics.GraphicsDevice, VertexPositionColor.VertexElements
      );

      this.primitiveBatch = new PrimitiveBatch<VertexPositionColor>(
        GraphicsDevice, this.vertexDeclaration, VertexPositionColor.SizeInBytes
      );
#endif
      this.basicEffectContext = new BasicEffectDrawContext(
        this.graphics.GraphicsDevice
      );

      // Create a new camera with a perspective projection matrix
      this.camera = new Camera(
        Matrix.CreateLookAt(
          new Vector3(0.0f, 0.0f, 25.0f), // camera location
          new Vector3(10.0f, 10.0f, 0.0f), // camera focal point
          Vector3.Up // up vector for the camera's orientation
        ),
        Matrix.CreatePerspectiveFieldOfView(
          MathHelper.PiOver4, // field of view
          (float)Window.ClientBounds.Width / (float)Window.ClientBounds.Height, // aspect ratio
          0.01f, 1000.0f // near and far clipping plane
        )
      );

      base.Initialize();
    }

    /// <summary>Immediately releases all resources owned by the instance</summary>
    /// <param name="disposing">Whether the call was initiated by user code</param>
    protected override void Dispose(bool disposing) {
      if(disposing) {
        if(this.primitiveBatch != null) {
          this.primitiveBatch.Dispose();
          this.primitiveBatch = null;
        }
        if(this.vertexDeclaration != null) {
          this.vertexDeclaration.Dispose();
          this.vertexDeclaration = null;
        }
      }
      base.Dispose(disposing);
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent() {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);

      // TODO: use this.Content to load your game content here
      this.font = Content.Load<SpriteFont>("Test");
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// all content.
    /// </summary>
    protected override void UnloadContent() {
      // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime) {
      // Allows the game to exit
      if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        this.Exit();

      // TODO: Add your update logic here
      this.camera.HandleControls(gameTime);

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.Gray);

      this.basicEffectContext.BasicEffect.View = this.camera.View;
      this.basicEffectContext.BasicEffect.Projection = this.camera.Projection;
      this.basicEffectContext.BasicEffect.World = Matrix.Identity;
      
      //GraphicsDevice.RenderState.CullMode = CullMode.None;
      this.basicEffectContext.BasicEffect.VertexColorEnabled = true;
      //this.basicEffectContext.BasicEffect.EnableDefaultLighting = true;
      
      #if false
      this.spriteBatch.Begin();
      this.spriteBatch.DrawString(
        this.font, "Hello World", new Vector2(100.0f, 100.0f), Color.White
      );
      this.spriteBatch.End();
      #endif

      this.primitiveBatch.Begin(QueueingStrategy.Deferred);

      for(int y = 0; y < 60; ++y) {
        for(int x = 0; x < 60; ++x) {
          Vector3 tl = new Vector3(x * 10.0f, y * 10.0f, 0.0f);
          Vector3 tr = new Vector3(x * 10.0f + 9.0f, y * 10.0f, 0.0f);
          Vector3 bl = new Vector3(x * 10.0f, y * 10.0f + 9.0f, 0.0f);
          Vector3 br = new Vector3(x * 10.0f + 9.0f, y * 10.0f + 9.0f, 0.0f);

          this.tileVertices[0] = new VertexPositionColor(tl, Color.White);
          this.tileVertices[1] = new VertexPositionColor(bl, Color.White);
          this.tileVertices[2] = new VertexPositionColor(br, Color.White);
          this.tileVertices[3] = new VertexPositionColor(tl, Color.White);
          this.tileVertices[4] = new VertexPositionColor(br, Color.White);
          this.tileVertices[5] = new VertexPositionColor(tr, Color.White);

          this.primitiveBatch.Draw(this.tileVertices, this.basicEffectContext);
        }
      }
      this.primitiveBatch.End();

      // TODO: Add your drawing code here
      this.spriteBatch.Begin();
      this.spriteBatch.DrawString(
        this.font, "Rendering 3600 dynamic quads (21600 vertices)",
        new Vector2(10.0f, 25.0f), Color.Yellow
      );
      this.spriteBatch.DrawString(
        this.font, "Use WASD RF to move, 8462 79 to rotate.",
        new Vector2(10.0f, 50.0f), Color.Yellow
      );
      this.spriteBatch.End();

      base.Draw(gameTime);
    }

    /// <summary>Used to construct a tile for rendering</summary>
    /// <remarks>
    ///   Reusing the same array for this prevents garbage from forming
    /// </remarks>
    private VertexPositionColor[/*6*/] tileVertices;

    /// <summary>Setups up and manages the graphics device</summary>
    private GraphicsDeviceManager graphics;
    /// <summary>XNA's sprite batcher (batches 2D sprites)</summary>
    private SpriteBatch spriteBatch;
    /// <summary>Camera managing the location of the viewer in the scene</summary>
    private Camera camera;
    /// <summary>Drawing context that uses the basic effect for rendering</summary>
    private BasicEffectDrawContext basicEffectContext;
    /// <summary>Vertex declaration for the position + color vertex</summary>
    private VertexDeclaration vertexDeclaration;
    /// <summary>Nuclex' primitive batcher (batches 3D polygons)</summary>
    private PrimitiveBatch<VertexPositionColor> primitiveBatch;
    /// <summary>Font used to display an explanation on the screen</summary>
    private SpriteFont font;

  }

} // namespace Nuclex.Graphics.Demo
