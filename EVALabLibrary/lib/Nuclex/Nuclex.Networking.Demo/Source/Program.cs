using System;
using System.Collections.Generic;

using Nuclex.Networking.Http;

namespace Nuclex.Networking.Demo {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("Press any key to perform a clean shutdown of the web server");

      using(HttpServer server = new HttpServer()) {
        server.Start();

        Console.ReadKey(true);

        server.Stop();
      }
    }
  }
}
