using System;
using Microsoft.Xna.Framework.Input;

namespace Nuclex.Input.Demo {
#if WINDOWS || XBOX
  static class Program {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args) {
      using (InputDemoGame game = new InputDemoGame()) {
        game.Run();
      }
    }
  }
#endif
}

