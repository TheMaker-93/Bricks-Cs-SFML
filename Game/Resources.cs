using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using SFML.Audio;

namespace TcGame
{
  public static class Resources
  {
    private static Dictionary<string, Texture> textures;
    private static Dictionary<string, Font> fonts;
    private static Dictionary<string, SoundBuffer> soundBuffers;

    public static Texture Texture(string name)
    {
      return textures[name];
    }

    public static Font Font(string name)
    {
      return fonts[name];
    }

    public static SoundBuffer Sound(string name)
    {
      return soundBuffers[name];
    }

    public static void LoadResources()
    {
      textures = new Dictionary<string, Texture>();
      fonts = new Dictionary<string, Font>();
      soundBuffers = new Dictionary<string, SoundBuffer>();

      string dir = AppDomain.CurrentDomain.BaseDirectory;
      (new FileInfo("Data/")).Directory.Create();
      var dirInfo = new DirectoryInfo(dir + "Data");

      LoadResources(dirInfo);
    }

    private static void LoadResources(DirectoryInfo dirInfo, string path = "")
    {
      LoadTextures(dirInfo, path);
      LoadFonts(dirInfo, path);
      LoadSounds(dirInfo, path);

      var subDirs = dirInfo.GetDirectories();
      foreach (DirectoryInfo subDir in subDirs)
      {
        LoadResources(subDir, path + subDir.Name + "/");
      }
    }

    private static void LoadTextures(DirectoryInfo dirInfo, string path)
    {
      string[] extensions = { "png", "jpg" };

      foreach (string ext in extensions)
      {
        FileInfo[] fileNames = dirInfo.GetFiles("*." + ext);

        foreach (var fileInfo in fileNames)
        {
          string name = fileInfo.Name.Remove(fileInfo.Name.IndexOf("." + ext));
          string key = path + name;
          var texture = new Texture("Data/" + key + "." + ext);

          textures.Add(key, texture);
        }
      }
    }

    private static void LoadFonts(DirectoryInfo dirInfo, string path)
    {
      FileInfo[] fileNames = dirInfo.GetFiles("*.ttf");

      foreach (var fileInfo in fileNames)
      {
        string name = fileInfo.Name.Remove(fileInfo.Name.IndexOf(".ttf"));
        string key = path + name;
        var font = new Font("Data/" + key + ".ttf");

        fonts.Add(key, font);
      }
    }

    private static void LoadSounds(DirectoryInfo dirInfo, string path)
    {
      FileInfo[] fileNames = dirInfo.GetFiles("*.wav");

      foreach (var fileInfo in fileNames)
      {
        string name = fileInfo.Name.Remove(fileInfo.Name.IndexOf(".wav"));
        string key = path + name;
        var soundBuffer = new SoundBuffer("Data/" + key + ".wav");

        soundBuffers.Add(key, soundBuffer);
      }
    }
  }
}
  