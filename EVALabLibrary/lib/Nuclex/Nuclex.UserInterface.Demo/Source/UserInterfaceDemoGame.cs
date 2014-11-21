using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Nuclex.UserInterface.Controls.Desktop;
using Nuclex.UserInterface.Visuals.Flat;
using Nuclex.Input;

namespace Nuclex.UserInterface.Demo {

  /// <summary>Demonstrates the capabilities of the Nuclex UserInterface library</summary>
  public class UserInterfaceDemoGame : Microsoft.Xna.Framework.Game {

    /// <summary>Initializes a new instance of the user interface demo</summary>
    public UserInterfaceDemoGame() {
      this.graphics = new GraphicsDeviceManager(this);
      this.input = new InputManager(Services, Window.Handle);
      this.gui = new GuiManager(Services);

      /*      
      var capturer = new Input.DefaultInputCapturer(this.input);
      capturer.ChangePlayerIndex(ExtendedPlayerIndex.Five);
      this.gui.InputCapturer = capturer;
      */

      /*      
      ((FlatGuiVisualizer)this.gui.Visualizer).RendererRepository.AddAssembly(
        typeof(UserInterfaceDemoGame).Assembly
      );
      */

      // Automatically query the input devices once per update
      Components.Add(this.input);

      // You can either add the GUI to the Components collection to have it render
      // automatically, or you can call the GuiManager's Draw() method yourself
      // at the appropriate place if you need more control.
      Components.Add(this.gui);

      IsMouseVisible = true;
    }

    /// <summary>
    ///   Allows the game to perform any initialization it needs to before starting to run.
    ///   This is where it can query for any required services and load any non-graphic
    ///   related content. Calling base.Initialize will enumerate through any components
    ///   and initialize them as well.
    /// </summary>
    protected override void Initialize() {
      base.Initialize();

      // Create a new screen. Screens manage the state of a GUI and accept input
      // notifications. If you have an in-game computer display where you want
      // to use a GUI, you can create a second screen for that and thus cleanly
      // separate the state of the in-game computer from your game's own GUI :)
      Viewport viewport = GraphicsDevice.Viewport;
      Screen mainScreen = new Screen(viewport.Width, viewport.Height);
      this.gui.Screen = mainScreen;

      // Each screen has a 'desktop' control. This invisible control by default
      // stretches across the whole screen and serves as the root of the control
      // tree in which all visible controls are managed. All controls are positioned
      // using a system of fractional coordinates and pixel offset coordinates.
      // We now adjust the position of the desktop window to prevent GUI or HUD
      // elements from appearing outside of the title-safe area.
      mainScreen.Desktop.Bounds = new UniRectangle(
        new UniScalar(0.1f, 0.0f), new UniScalar(0.1f, 0.0f), // x and y = 10%
        new UniScalar(0.8f, 0.0f), new UniScalar(0.8f, 0.0f) // width and height = 80%
      );

      // Create an instance of the demonstration dialog and add it to the desktop
      // control, which means it will become visible and interactive.
      mainScreen.Desktop.Children.Add(new DemoDialog());

      // Now let's do something funky: add buttons directly to the desktop.
      // This will also show the effect of the title-safe area.
      createDesktopControls(mainScreen);
    }

    /// <summary>
    ///   Load your graphics content. If loadAllContent is true, you should
    ///   load content from both ResourceManagementMode pools. Otherwise, just
    ///   load ResourceManagementMode.Manual content.
    /// </summary>
    protected override void LoadContent() {
      // TODO: Load any content
    }

    /// <summary>
    ///   Unload your graphics content. If unloadAllContent is true, you should
    ///   unload content from both ResourceManagementMode pools. Otherwise, just
    ///   unload ResourceManagementMode.Manual content. Manual content will get
    ///   Disposed by the GraphicsDevice during a Reset.
    /// </summary>
    protected override void UnloadContent() {
      Content.Unload();

      // TODO: Unload any content
    }

    /// <summary>
    ///   Allows the game to run logic such as updating the world,
    ///   checking for collisions, gathering input and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime) {
      // Allows the game to exit
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        this.Exit();

      // TODO: Add your update logic here

      base.Update(gameTime);
    }

    /// <summary>This is called when the game should draw itself.</summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime) {
      graphics.GraphicsDevice.Clear(Color.DarkGray);

      // TODO: Add your drawing code here
      base.Draw(gameTime);
    }

    /// <summary>
    ///   Creates the "New Game" and "Quit" buttons directly on the desktop
    /// </summary>
    /// <param name="mainScreen">
    ///   Screen to whose desktop the buttons will be added
    /// </param>
    private void createDesktopControls(Screen mainScreen) {

      // Button to open another "New Game" dialog
      ButtonControl newGameButton = new ButtonControl();
      newGameButton.Text = "New Game";
      newGameButton.Bounds = new UniRectangle(
        new UniScalar(1.0f, -190.0f), new UniScalar(1.0f, -32.0f), 100, 32
      );
      newGameButton.Pressed += delegate(object sender, EventArgs arguments) {
        // Insert at index 0 to make it the firstmost window
        this.gui.Screen.Desktop.Children.Insert(0, new DemoDialog());
      };
      mainScreen.Desktop.Children.Add(newGameButton);

      // Button through which the user can quit the application
      ButtonControl quitButton = new ButtonControl();
      quitButton.Text = "Quit";
      quitButton.Bounds = new UniRectangle(
        new UniScalar(1.0f, -80.0f), new UniScalar(1.0f, -32.0f), 80, 32
      );
      quitButton.Pressed += delegate(object sender, EventArgs arguments) { Exit(); };
      mainScreen.Desktop.Children.Add(quitButton);

    }

    /// <summary>Initializes and manages the graphics device</summary>
    private GraphicsDeviceManager graphics;
    /// <summary>Manages the graphical user interface</summary>
    private GuiManager gui;
    /// <summary>Manages input devices for the game</summary>
    private InputManager input;

  }

} // namespace Nuclex.UserInterface.Demo
