using SFML.Graphics;
using System;

namespace TcGame
{
  /// <summary>
  /// Contains util info for bricks
  /// </summary>
  public struct BrickMaterial
  {
    public Texture NormalTexture;
    public Texture BrokenTexture;
    public Type GiftType;
    public bool CanBeDestroyed;
    public bool CanBeBroken;
  }
}

