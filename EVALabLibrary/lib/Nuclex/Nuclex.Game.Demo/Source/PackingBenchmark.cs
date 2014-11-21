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

#if false

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

using Nuclex.Game.Packing;
using Nuclex.Support.Plugins;

using XnaPoint = Microsoft.Xna.Framework.Point;

namespace Nuclex.Game.Demo {

  public static class PackingBenchmark {

    static PackingBenchmark() {
      randomNumberGenerator = new Random();
    }

    public static void Run() {
      int[] dimensions = new int[] { 8, 16, 32, 64, 128, 256, 512 };

      System.Text.StringBuilder b = new System.Text.StringBuilder();

      // Warmup run
      for(int i = 0; i < 10; ++i) {
        RectanglePacker packer = new ArevaloRectanglePacker(
          1024, 1024
        );
        measurePacker(packer, 128, 512);
      }

      for(int d = dimensions.Length - 1; d > 0; --d) {
        TimeSpan total = TimeSpan.Zero;
        for(int i = 0; i < 100; ++i) {
          RectanglePacker packer = new ArevaloRectanglePacker(
            1024, 1024
          );

          total += measurePacker(packer, dimensions[d - 1], dimensions[d]);
        }

        TimeSpan average = TimeSpan.FromTicks(total.Ticks / 100);

        b.AppendFormat(
          "Size range {0}-{1} took {2} milliseconds on average\n",
          dimensions[d - 1], dimensions[d], average
        );

      }
      
      System.Windows.Forms.MessageBox.Show(b.ToString());

    }

    /// <summary>
    ///   Measures how long a packer needs to fill the entire packing area
    /// </summary>
    /// <param name="packer">Packer that will be measured</param>
    /// <param name="mininum">Minimum size of the packed rectangles</param>
    /// <param name="mininum">Maximum size of the packed rectangles</param>
    /// <returns>The time taken by the packer to fill the packing area</returns>
    private static TimeSpan measurePacker(
      RectanglePacker packer, int minimum, int maximum
    ) {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      // Place as many random rectangles as possible into the packing area,
      // stopping as soon as the packer runs out of space once.
      for(int count = 0; ; ++count) {
        Size dimensions = createRandomDimensions(minimum, maximum);

        // Let the packer find a place for the randomly sized rectangle
        XnaPoint placement;
        bool result = packer.TryPack(
          dimensions.Width, dimensions.Height, out placement
        );

        // If the packer was unable to find a location at which the rectangle
        // could be placed, the packing area is considered full and we will stop
        if(!result) {
          break;
        }
      }

      stopwatch.Stop();
      return stopwatch.Elapsed;
    }

    /// <summary>
    ///   Creates random dimensions for a rectangle according to the sizes specified
    ///   by the user through the dialog controls
    /// </summary>
    /// <param name="mininum">Minimum size of the packed rectangles</param>
    /// <param name="mininum">Maximum size of the packed rectangles</param>
    /// <returns>The dimensions of a random rectangle</returns>
    private static Size createRandomDimensions(int mininum, int maximum) {
      int width = randomNumberGenerator.Next(16, 32);
      int height = randomNumberGenerator.Next(16, 32);

      return new Size(width, height);
    }

    /// <summary>Random number generator used to generate the rectangles</summary>
    private static Random randomNumberGenerator;

  }

} // namespace Nuclex.Game.Demo

#endif
