using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using Nuclex.Graphics;

namespace Nuclex.Fonts.Demo.ShowOff {

  /// <summary>Component for showing off the capabilities of Nuclex.Fonts</summary>
  public class FontShowOffComponent : DrawableGameComponent {

    /// <summary>Initializes a new font showoff component</summary>
    /// <param name="game">Game providing containing the graphics device to use</param>
    public FontShowOffComponent(Microsoft.Xna.Framework.Game game)
      : base(game) {

      this.contentManager = new ContentManager(game.Services);
    }

    /// <summary>
    ///   Allows the game component to perform any initialization it needs to before starting
    ///   to run. This is where it can query for any required services and load content.
    /// </summary>
    public override void Initialize() {

      base.Initialize();
    }

    /// <summary>Allows the game component to update itself.</summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public override void Update(GameTime gameTime) {
      // TODO: Add your update code here

      // Plan:
      //
      // Screen #1
      //
      //   Nuclex.Fonts is a component from the Nuclex Framework.
      //
      //   Its content importer can act as a replacement for
      //   the XNA font importer. Compare these fonts, both
      //   rendered using the XNA SpriteFont class:
      //
      //   __baseline______________________Nuclex Importer____
      //                   XNA Importer 
      //
      //   <cool fadeout>
      //
      // Screen #2
      //
      //   But the real strength of Nuclex.Fonts is something else:
      //   Vector-based font rendering. 
      //
      //   For some effects, bitmap fonts just don't cut it:
      //
      //     Rotating SpriteFont           Rotating VectorFont
      //   
      //   <replace>
      //
      //     Zooming SpriteFont             Zooming VectorFont
      //
      //   <replace>
      //
      //             Particles generated on Text surface

      base.Update(gameTime);
    }

/*
    /// <summary>
    ///   Called when the object needs to set up graphics resources. Override to
    ///   set up any object specific graphics resources.
    /// </summary>
    /// <param name="loadAllContent">
    ///   True if all graphics resources need to be set up; false if only
    ///   manual resources need to be set up.
    /// </param>
    protected override void LoadGraphicsContent(bool loadAllContent) {
      base.LoadGraphicsContent(loadAllContent);
    }

    /// <summary>
    ///   Called when graphics resources should be released. Override to
    ///   handle component specific graphics resources.
    /// </summary>
    /// <param name="unloadAllContent">
    ///   True if all graphics resources should be released; false if only
    ///   manual resources should be released.
    /// </param>
    protected override void UnloadGraphicsContent(bool unloadAllContent) {
      base.UnloadGraphicsContent(unloadAllContent);
    }
*/

    /// <summary>Loads and manages the component's assets</summary>
    private ContentManager contentManager;

  }

} // namespace Nuclex.Fonts.Demo.ShowOff
