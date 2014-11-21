using System;

namespace Nuclex.Fonts.Demo {
  static class Program {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args) {
      using(FontDemoGame game = new FontDemoGame()) {
        game.Run();
      }
    }
  }
}

