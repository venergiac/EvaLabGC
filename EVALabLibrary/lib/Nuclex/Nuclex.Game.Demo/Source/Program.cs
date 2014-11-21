#region CPL License
/*
Nuclex Framework
Copyright (C) 2002-2009 Nuclex Development Labs

This library is free software; you can redistribute it and/or
modify it under the terms of the IBM Common Public License as
published by the IBM Corporation; either version 1.0 of the
License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
IBM Common Public License for more details.

You should have received a copy of the IBM Common Public
License along with this library
*/
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Nuclex.Game.Demo {

  /// <summary>Contains the program's start up</summary>
  static class Program {

    /// <summary>The main entry point for the application.</summary>
    [STAThread]
    static void Main() {
      //PackingBenchmark.Run();
      //return;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new DemoSelectorForm());
    }

  }

} // namespace Nuclex.Game.Demo
