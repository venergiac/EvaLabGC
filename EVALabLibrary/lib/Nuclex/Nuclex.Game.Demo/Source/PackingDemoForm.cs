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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Nuclex.Game.Packing;
using Nuclex.Support.Plugins;

using XnaPoint = Microsoft.Xna.Framework.Point;

namespace Nuclex.Game.Demo {

  /// <summary>Demonstrates the rectangle packing algorithms</summary>
  public partial class PackingDemoForm : Form {

    #region interface IRectanglePackerFactory

    /// <summary>Interface for a rectangle packer factory</summary>
    private interface IRectanglePackerFactory {

      /// <summary>Human readable name of the rectangle packer</summary>
      string PackerName { get; }

      /// <summary>Creates a new packer for a packing area of the specified size</summary>
      /// <param name="packingAreaWidth">Width of the packing area</param>
      /// <param name="packingAreaHeight">Height of the packing area</param>
      /// <returns>The newly created rectangle packer</returns>
      RectanglePacker CreatePacker(int packingAreaWidth, int packingAreaHeight);

    }

    #endregion // interface IRectanglePackerFactory

    #region class DynamicRectanglePackerFactory

    /// <summary>
    ///   Dynamic factory that can be configured to produce any packer by reflection
    /// </summary>
    private class DynamicRectanglePackerFactory : IRectanglePackerFactory {

      /// <summary>Initializes a new dynamic rectangle packer factory</summary>
      /// <param name="packerType">
      ///   Type of the rectangle packer that will be produced
      /// </param>
      public DynamicRectanglePackerFactory(Type packerType) {
        this.packerType = packerType;
      }

      /// <summary>Determines whether the factory </summary>
      /// <param name="packerType">
      ///   Type of the packer whose compatibility will be checked
      /// </param>
      /// <returns>True if the packer is compatible for this factory</returns>
      public static bool IsCompatible(Type packerType) {
        return
          (!packerType.IsAbstract) &&
          typeof(RectanglePacker).IsAssignableFrom(packerType) &&
          (getPackerConstructor(packerType) != null);
      }

      /// <summary>Human-readable name of the rectangle packer</summary>
      public string PackerName {
        get {
          string typeName = this.packerType.Name;

          // Count the number of uppercase characters in the string
          int uppercaseCharacterCount = 0;
          for(int index = 0; index < typeName.Length; ++index) {
            if(char.IsUpper(typeName, index)) {
              ++uppercaseCharacterCount;
            }
          }

          // Now we can create a string builder with the exact required capacity
          StringBuilder nameBuilder = new StringBuilder(
            typeName.Length + uppercaseCharacterCount
          );

          // Feed the type name into the string builder and each time we see an
          // uppercase character, prefix it with the space character.
          for(int index = 0; index < typeName.Length; ++index) {
            if((index != 0) && char.IsUpper(typeName, index)) {
              nameBuilder.Append(' ');
            }
            nameBuilder.Append(typeName[index]);
          }

          return nameBuilder.ToString();
        }
      }

      /// <summary>
      ///   Creates a packer for a packing area of the specified dimensions
      /// </summary>
      /// <param name="packingAreaWidth">Width of the packing area</param>
      /// <param name="packingAreaHeight">Height of the packing area</param>
      /// <returns>The newly creating rectangle packer</returns>
      public RectanglePacker CreatePacker(int packingAreaWidth, int packingAreaHeight) {
        ConstructorInfo constructor = getPackerConstructor(this.packerType);
        if(constructor == null) {
          throw new InvalidOperationException(
            "The selected packing algorithm is not compatible with the factory"
          );
        }

        return (RectanglePacker)constructor.Invoke(
          new object[] { packingAreaWidth, packingAreaHeight }
        );
      }

      /// <summary>
      ///   Retrieves the constructor of the packer the generic variant of this class
      ///   has been specialized for
      /// </summary>
      /// <param name="packerType">
      ///   Type of the packer whose constructor will be returned
      /// </param>
      /// <returns>The constructor of the provided packer type</returns>
      private static ConstructorInfo getPackerConstructor(Type packerType) {
        return packerType.GetConstructor(
          new Type[] { typeof(int), typeof(int) }
        );
      }

      /// <summary>Type of the packer this factory instance is constructing</summary>
      private Type packerType;

    }

    #endregion // class DynamicRectanglePackerFactory

    #region class RectanglePackerEmployer

    /// <summary>Employs factories for all compatible rectangle packers</summary>
    private class RectanglePackerEmployer : Employer {

      /// <summary>Initializes a new rectangle packer employer</summary>
      public RectanglePackerEmployer() {
        this.factories = new List<IRectanglePackerFactory>();
      }

      /// <summary>Determines whether the type suites the employer's requirements</summary>
      /// <param name="type">Type that is checked for employability</param>
      /// <returns>True if the type can be employed</returns>
      public override bool CanEmploy(Type type) {
        return DynamicRectanglePackerFactory.IsCompatible(type);
      }

      /// <summary>Employs the specified plugin type</summary>
      /// <param name="type">Type to be employed</param>
      public override void Employ(Type type) {
        if(!CanEmploy(type)) {
          throw new ArgumentException("Not a compatible rectangle packer", "type");
        }

        this.factories.Add(new DynamicRectanglePackerFactory(type));
      }

      /// <summary>Factories for the employed rectangle packers</summary>
      public List<IRectanglePackerFactory> Factories {
        get { return this.factories; }
      }

      /// <summary>Factories for the rectangle packer we have employed</summary>
      private List<IRectanglePackerFactory> factories;

    }

    #endregion // class RectanglePackerEmployer

    /// <summary>Initializes a new rectangle packing demonstration form</summary>
    public PackingDemoForm() {
      InitializeComponent();

      this.randomNumberGenerator = new Random();

      this.employer = new RectanglePackerEmployer();
      this.pluginHost = new PluginHost(this.employer);

      // Employ our own assembly in order to obtain the default GUI renderers
      this.pluginHost.Repository.AddAssembly(typeof(RectanglePacker).Assembly);

      initializePackerCombo();
      packingViewResized(this.packingViewPicture, EventArgs.Empty);
    }

    /// <summary>Renders the packed rectangles into the packing view picture box</summary>
    /// <param name="sender">Picture box that is being redrawn</param>
    /// <param name="arguments">
    ///   Contains the graphics interface through which we can draw into the picture box
    /// </param>
    private void paintPackingViewPicture(object sender, PaintEventArgs arguments) {
      if(this.packedRectangles == null) {
        return;
      }

      arguments.Graphics.FillRectangles(
        Brushes.LightGray, this.packedRectangles.ToArray()
      );
      arguments.Graphics.DrawRectangles(
        Pens.DarkBlue, this.packedRectangles.ToArray()
      );

      StringFormat format = StringFormat.GenericDefault;
      format.Alignment = StringAlignment.Far;

      string text = string.Format(
        "Rectangles: {0}\nTime: {1} ms",
        this.packedRectangles.Count,
        (int)this.packingTime.TotalMilliseconds
      );

      arguments.Graphics.DrawString(
        text,
        Font,
        Brushes.Black,
        floatRectangleFromRectangle(this.packingViewPicture.DisplayRectangle),
        format
      );
    }

    /// <summary>Updates the size label for the packing view</summary>
    /// <param name="sender">Packing view picture box that has been resized</param>
    /// <param name="arguments">Not used</param>
    private void packingViewResized(object sender, EventArgs arguments) {
      this.areaLabel.Text = string.Format(
        "Size: {0} x {1}",
        this.packingViewPicture.ClientSize.Width,
        this.packingViewPicture.ClientSize.Height
      );
    }

    /// <summary>Called when the pack button has been clicked on</summary>
    /// <param name="sender">Button that has been clicked on</param>
    /// <param name="arguments">Not used</param>
    private void packButtonClicked(object sender, EventArgs arguments) {
      IRectanglePackerFactory factory = this.employer.Factories[
        this.packingAlgorithmCombo.SelectedIndex
      ];
      RectanglePacker packer = factory.CreatePacker(
        this.packingViewPicture.ClientSize.Width,
        this.packingViewPicture.ClientSize.Height
      );
      List<Rectangle> packedRectangles = new List<Rectangle>();

      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      // Place as many random rectangles as possible into the packing area,
      // stopping as soon as the packer runs out of space once.
      for(; ; ) {
        Size dimensions = createRandomDimensions();

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

        // Add the packed rectangle into our list. We reduce the dimensions by one
        // because the .NET drawing routines draw the rectangles with the borders
        // inclusive, meaning a 1x1 pixel rectangle consumes 4 pixels when drawn
        packedRectangles.Add(
          new Rectangle(
            placement.X, placement.Y,
            dimensions.Width - 1, dimensions.Height - 1
          )
        );
      }

      stopwatch.Stop();

      // Take over the new packed rectangle list and tell the packing view picture box
      // that it needs to be redrawn
      this.packedRectangles = packedRectangles;
      this.packingTime = stopwatch.Elapsed;
      this.packingViewPicture.Invalidate();
    }

    /// <summary>
    ///   Creates random dimensions for a rectangle according to the sizes specified
    ///   by the user through the dialog controls
    /// </summary>
    /// <returns>The dimensions of a random rectangle</returns>
    private Size createRandomDimensions() {
      int width = this.randomNumberGenerator.Next(
        (int)this.minimumWidthEdit.Value,
        (int)this.maximumWidthEdit.Value
      );
      int height = this.randomNumberGenerator.Next(
        (int)this.minimumHeightEdit.Value,
        (int)this.maximumHeightEdit.Value
      );

      return new Size(width, height);
    }

    /// <summary>Initializes a the packer combo box</summary>
    private void initializePackerCombo() {
      this.packingAlgorithmCombo.Items.Clear();

      // Add the names packers for which we have factories to the combo box
      for(int index = 0; index < this.employer.Factories.Count; ++index) {
        this.packingAlgorithmCombo.Items.Add(
          this.employer.Factories[index].PackerName
        );
      }

      // If the combo box is non-empty, select the first entry so it isn't in
      // an invalid state when the dialog is shown
      if(this.employer.Factories.Count > 0) {
        this.packingAlgorithmCombo.SelectedIndex = 0;
      }
    }

    /// <summary>Converts an integer rectangle into a floating point rectangle</summary>
    /// <param name="rectangle">Integer rectangle that will be converted</param>
    /// <returns>
    ///   A floating point rectangle that is equivalent to the integer rectangle
    /// </returns>
    private RectangleF floatRectangleFromRectangle(Rectangle rectangle) {
      return new RectangleF(
        (float)rectangle.X,
        (float)rectangle.Y,
        (float)rectangle.Width,
        (float)rectangle.Height
      );
    }

    /// <summary>Random number generator used to generate the rectangles</summary>
    private Random randomNumberGenerator;
    /// <summary>Employer used to find compatible rectangle packers</summary>
    private RectanglePackerEmployer employer;
    /// <summary>Hosts any plugins loaded into the process</summary>
    private PluginHost pluginHost;
    /// <summary>The packed rectangles from the last packer run</summary>
    private List<Rectangle> packedRectangles;
    /// <summary>Time needed for the last packing run</summary>
    private TimeSpan packingTime;

  }

} // namespace Nuclex.Game.Demo
