using System;

namespace Nuclex.Graphics.Demo {
  static class Program {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args) {
      using(GraphicsDemoGame game = new GraphicsDemoGame()) {
        game.Run();
      }
    }
  }
}

