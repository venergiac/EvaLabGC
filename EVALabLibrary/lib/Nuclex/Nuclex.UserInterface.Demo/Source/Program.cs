using System;

using Microsoft.Xna.Framework;

namespace Nuclex.UserInterface.Demo {

  /// <summary>Containsthe program's startup code</summary>
  static class Program {
  
    /// <summary>Main entry point for the application</summary>
    static void Main(string[] args) {
      using(UserInterfaceDemoGame game = new UserInterfaceDemoGame()) {
        game.Run();
      }
    }

  }

} // namespace Nuclex.UserInterface.Demo
