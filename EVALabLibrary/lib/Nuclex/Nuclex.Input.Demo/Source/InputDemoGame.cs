using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Nuclex.Input.Devices;
using Nuclex.Support;

namespace Nuclex.Input.Demo {

  /// <summary>Game that demonstrates the usage of Nuclex.Input</summary>
  public class InputDemoGame : Microsoft.Xna.Framework.Game {

    /// <summary>Initializes a new input demonstration game</summary>
    public InputDemoGame() {
      this.graphics = new GraphicsDeviceManager(this);
      this.inputManager = new InputManager(Services, Window.Handle);

      Content.RootDirectory = "Content";
    }

    /// <summary>
    ///   Allows the game to perform any initialization it needs to before starting to run.
    ///   This is where it can query for any required services and load any non-graphic
    ///   related content. Calling base.Initialize will enumerate through any components
    ///   and initialize them as well.
    /// </summary>
    protected override void Initialize() {
      base.Initialize();

      // Whenever a key is pressed on the keyboard, call the charEntered() method
      // (see below) so the game can do something with the character.
      IKeyboard keyboard = this.inputManager.GetKeyboard();
      keyboard.CharacterEntered += new Devices.CharacterDelegate(charEntered);

      IGamePad gamePad = this.inputManager.GetGamePad(PlayerIndex.One);
      gamePad.ButtonPressed += new GamePadButtonDelegate(gamePadButtonPressed);
    }

    /// <summary>Called when a character is entered on the keyboard</summary>
    /// <param name="character">Character that has been entered</param>
    private void charEntered(char character) {
      if (character == '\b') { // backspace
        if (this.userInputStringBuilder.Length > 0) {
          this.userInputStringBuilder.Remove(this.userInputStringBuilder.Length - 1, 1);
        }
      } else {
        this.userInputStringBuilder.Append(character);
      }
    }

    /// <summary>Called when the player presses a button on the first game pad</summary>
    /// <param name="buttons">Button(s) that have been pressed</param>
    private void gamePadButtonPressed(Microsoft.Xna.Framework.Input.Buttons buttons) {

      // End the game if the player has pressed the Back button
      if ((buttons & Microsoft.Xna.Framework.Input.Buttons.Back) != 0) {
        Exit();
      }

    }

    /// <summary>
    ///   LoadContent will be called once per game and is the place to load
    ///   all of your content.
    /// </summary>
    protected override void LoadContent() {

      // Create a new SpriteBatch, which can be used to draw textures.
      this.spriteBatch = new SpriteBatch(GraphicsDevice);

      // Load the font we'll be using for the text display
      this.displayFont = Content.Load<SpriteFont>("DisplayFont");

    }

    /// <summary>
    ///   UnloadContent will be called once per game and is the place to unload
    ///   all content.
    /// </summary>
    protected override void UnloadContent() {
      Content.Unload();

      if (this.spriteBatch != null) {
        this.spriteBatch.Dispose();
        this.spriteBatch = null;
      }
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime) {
      base.Update(gameTime);

      // Update the status of all input devices
      this.inputManager.Update();

      // Retrieve the state of the first connected Xbox 360 game pad
      // (Hint: DirectInput devices are at index 5-8 if you want to try)
      IGamePad firstGamePad = this.inputManager.GetGamePad(ExtendedPlayerIndex.One);
      this.state = firstGamePad.GetExtendedState();
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin();
      {
        // Print out the state of the game pad
        printGamePadState();

        // Display the text the user has entered so far
        printKeyboardInput();
      }
      spriteBatch.End();

      base.Draw(gameTime);
    }

    /// <summary>what the user has entered on the keyboard so far</summary>
    private void printKeyboardInput() {
      spriteBatch.DrawString(
        this.displayFont, "Text input:", new Vector2(50, 425), Color.LightGreen
      );

      Vector2 size = this.displayFont.MeasureString("Text input: ");
      spriteBatch.DrawString(
        this.displayFont, this.userInputStringBuilder,
        new Vector2(50 + size.X, 425), Color.LightGreen
      );
    }

    /// <summary>Displays the current state of the game pad</summary>
    private void printGamePadState() {

      // Device name
      {
        tempStringBuilder.Remove(0, tempStringBuilder.Length);
        tempStringBuilder.AppendFormat(
          "Device: {0}",
          inputManager.GetGamePad(ExtendedPlayerIndex.One).Name
        );
        spriteBatch.DrawString(
          displayFont, tempStringBuilder, new Vector2(50, 50), Color.Yellow
        );
      }

      // Axes
      {
        tempStringBuilder.Remove(0, tempStringBuilder.Length);
        tempStringBuilder.AppendFormat("Provided axes: {0}", state.AvailableAxes);
        spriteBatch.DrawString(
          displayFont, tempStringBuilder, new Vector2(50, 100), Color.White
        );

        tempStringBuilder.Remove(0, tempStringBuilder.Length);
        foreach (ExtendedAxes axis in EnumHelper.GetValues<ExtendedAxes>()) {
          if ((state.AvailableAxes & axis) == axis) {
            tempStringBuilder.AppendFormat("{0}:{1} ", axis, state.GetAxis(axis));
          }
        }
        spriteBatch.DrawString(
          displayFont, tempStringBuilder, new Vector2(50, 125), Color.White
        );
      }

      // Axes
      {
        tempStringBuilder.Remove(0, tempStringBuilder.Length);
        tempStringBuilder.AppendFormat("Provided sliders: {0}", state.AvailableSliders);
        spriteBatch.DrawString(
          displayFont, tempStringBuilder, new Vector2(50, 175), Color.White
        );

        tempStringBuilder.Remove(0, tempStringBuilder.Length);
        foreach (ExtendedSliders slider in EnumHelper.GetValues<ExtendedSliders>()) {
          if ((state.AvailableSliders & slider) == slider) {
            tempStringBuilder.AppendFormat("{0}:{1} ", slider, state.GetSlider(slider));
          }
        }
        spriteBatch.DrawString(
          displayFont, tempStringBuilder, new Vector2(50, 200), Color.White
        );
      }

      // Buttons
      {
        tempStringBuilder.Remove(0, tempStringBuilder.Length);
        tempStringBuilder.AppendFormat("Number of buttons: {0}", state.ButtonCount);
        spriteBatch.DrawString(
          displayFont, tempStringBuilder, new Vector2(50, 250), Color.White
        );

        tempStringBuilder.Remove(0, tempStringBuilder.Length);
        for (int index = 0; index < state.ButtonCount; ++index) {
          if (state.IsButtonDown(index)) {
            tempStringBuilder.Append('X');
          } else {
            tempStringBuilder.Append('.');
          }
        }
        spriteBatch.DrawString(
          displayFont, tempStringBuilder, new Vector2(50, 275), Color.White
        );
      }

      // PoVs
      {
        tempStringBuilder.Remove(0, tempStringBuilder.Length);
        tempStringBuilder.AppendFormat(
          "Number of Point-of-View hats: {0}", state.PovCount
        );
        spriteBatch.DrawString(
          displayFont, tempStringBuilder, new Vector2(50, 325), Color.White
        );

        tempStringBuilder.Remove(0, tempStringBuilder.Length);
        tempStringBuilder.AppendFormat(
          "Pov1:{0} Pov2:{1} Pov3:{2} Pov4:{3}",
          state.Pov1, state.Pov2, state.Pov3, state.Pov4
        );
        spriteBatch.DrawString(
          displayFont, tempStringBuilder, new Vector2(50, 350), Color.White
        );
      }

    }

    /// <summary>Initializes and manages the graphics device used for rendering</summary>
    private GraphicsDeviceManager graphics;
    /// <summary>Batches sprites and text for efficient rendering</summary>
    private SpriteBatch spriteBatch;
    /// <summary>Polls input devices and allows their state to be queried</summary>
    private InputManager inputManager;
    /// <summary>Font used to display text on the screen</summary>
    private SpriteFont displayFont;

    /// <summary>Used to store the most recent state of the game pad</summary>
    private ExtendedGamePadState state;
    /// <summary>Temporary string builder used for various purposes</summary>
    private StringBuilder tempStringBuilder = new StringBuilder();
    /// <summary>Contains the text the user has entered on the keyboard</summary>
    private StringBuilder userInputStringBuilder = new StringBuilder();

  }

} // namespace Nuclex.Input.Demo
