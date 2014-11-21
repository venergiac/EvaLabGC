using System;
using System.Collections.Generic;
using System.Text;

using Nuclex.Audio.Metadata;
using Nuclex.Support.Tracking;
using Nuclex.Support.Scheduling;

namespace Nuclex.Audio.Demo {

  /// <summary>Contains the main program code</summary>
  class Program {

    /// <summary>Main entry point of the application</summary>
    /// <param name="args">Command line arguments passed to the application</param>
    static void Main(string[] args) {

      // Credentials with which we are going to log in to the server
      Cddb.Credentials credentials = new Cddb.Credentials(
        "johndoe",
        "abc.fubar.com",
        "testing",
        "v0.0PL0"
      );

      // Log in to a random freedb server
      Console.Write("Connecting...");
      CddbConnection connection = Cddb.Connect(credentials).Join();
      Console.WriteLine("Done");

      // Print out some informations about the server
      Console.WriteLine("Connected to " + connection.Hostname);
      Console.WriteLine("Running CDDB software version " + connection.Version);

      // Switch to the best possible protocol level of the CDDB protocol
      // (higher levels provide more features, since level 6, text is UTF-8 encoded,
      // allowing for any unicode character to occur in the transmitted data)
      Console.WriteLine(new string('=', 78));
      upgradeProtocolLevel(connection);

      // Example: List the categories known to the server
      Console.WriteLine(new string('=', 78));
      listCategories(connection);

      // Example: Query for known data about a specific CD
      Console.WriteLine(new string('=', 78));
      Cddb.Disc exampleDisc = queryExampleAlbum(connection);

      // Example: Retrieve the XMCD database entry for an album
      Console.WriteLine(new string('=', 78));
      readExampleAlbum(connection, exampleDisc);

      // Close the connection again
      connection.Quit().Join();
      connection.Dispose();

    }

    /// <summary>Upgrades the CDDB protocol to the best level we can use</summary>
    /// <param name="connection">Connection whose protocol level will be upgraded</param>
    private static void upgradeProtocolLevel(CddbConnection connection) {

      // Optimal protocol level for this application
      const int OptimalLevel = 6;

      // Retrieve the current and maximum supported protocol level
      Console.Write("Retrieving current protocol level...");
      CddbConnection.ServerProtocolLevel level = connection.GetProtocolLevel().Join();
      Console.WriteLine("Done");

      // Find the best level to use
      int newLevel = OptimalLevel;
      if(level.SupportedProtocolLevel.HasValue) {
        Console.WriteLine(
          string.Format(
            "Maximum supported level is {0}", level.SupportedProtocolLevel.Value
          )
        );

        // Use the highest level we can get, up to 6 (since this software has
        // not been tested with any later levels)
        newLevel = Math.Min(6, level.SupportedProtocolLevel.Value);
      }

      // Best level found, now apply it
      Console.Write("Changing protocol level...");
      connection.ChangeProtocolLevel(newLevel).Join();
      Console.WriteLine("Done");

    }

    /// <summary>Lists the musical categories that are known to the CDDB server</summary>
    /// <param name="connection">CDDB server connection that will be queried</param>
    private static void listCategories(CddbConnection connection) {

      // Ask the server to list its known musical categories
      Console.Write("Retrieving category list...");
      string[] categories = connection.ListCategories().Join();
      Console.WriteLine("Done");

      // List the categories we received
      Console.WriteLine("Known musical categories on this server:");
      for(int index = 0; index < categories.Length; ++index) {
        Console.WriteLine("\t" + categories[index]);
      }

    }

    /// <summary>Queries the server for an example album</summary>
    /// <param name="connection">
    ///   Connection through which the query will take place
    /// </param>
    private static Cddb.Disc queryExampleAlbum(CddbConnection connection) {

      // Query the server for an example album that will result in
      // in inexact match with multiple candidates (at least at the time
      // of this writing :p)
      Console.Write("Querying data for example album...");
      Request<Cddb.Disc[]> queryRequest = connection.Query(
        3386, // 56:26.09
        new int[] {
          0, // 0:00.00
          213, // 3:33.20
          439, // 7:19.39
          642, // 10:42.06
          900, // 15:00.25
          1147, // 19:07.42
          1429, // 23:49.46
          1660, // 27:40.13
          2019, // 33:39.37
          2226, // 37:06.41
          2481, // 41:21.45
          2772, // 46:11.55
          2960, // 49:19.58
          3209, // 53:29.49
        }
      );

      // The request is now executing asynchronously, since we didn't call .Join()
      // to wait for it to finish yet. If you were writing a CDDB application, now
      // would be the time to display the freedb or CDDB banner to the user.
      Console.Write(" [display banner here] ");

      // Wait for the request to finish and retrieve the query results
      Cddb.Disc[] discs = queryRequest.Join();
      Console.WriteLine("Done");

      // Print out the list of matching albums we received
      Console.WriteLine("Matching albums retrieved from the server:");
      for(int index = 0; index < discs.Length; ++index) {
        Console.WriteLine(
          string.Format(
            "\t0x{0:x8}: {1} - {2} ({3})",
            discs[index].DiscId,
            discs[index].Artist,
            discs[index].Title,
            discs[index].Category
          )
        );
      }

      // We will need this for further processing
      return discs[0];

    }

    /// <summary>
    ///   Reads the XMCD database entry for an example album from the server
    /// </summary>
    /// <param name="connection">
    ///   Connection through which the read request will be issued
    /// </param>
    /// <param name="disc">Disc informations of the example album</param>
    private static void readExampleAlbum(CddbConnection connection, Cddb.Disc disc) {

      // Read the XMCD database entry from the CDDB server
      Console.Write("Reading database entry...");
      Cddb.DatabaseEntry entry = connection.Read(disc.Category, disc.DiscId).Join();
      Console.WriteLine("Done");

      // Print out some informations about the album
      Console.WriteLine("\tArtist name: " + entry.Artist);
      Console.WriteLine("\tAlbum title: " + entry.Album);
      Console.WriteLine("\tProduction year: " + entry.Year.ToString());
      Console.WriteLine("\tLength: " + entry.DiscLengthSeconds.ToString() + " seconds");

      // Print out the track list
      Console.WriteLine("\tTracks:");
      for(int index = 0; index < entry.Tracks.Length; ++index) {
        Console.WriteLine("\t\t" + entry.Tracks[index].Title);
      }

    }

  }

} // namespace Nuclex.Audio.Demo
