using SFML.Graphics;

namespace TcGame
{
  public class Background : StaticActor
  {
    public Background()
    {
      Sprite = new Sprite(Resources.Texture("Arkanoid/Textures/Background/background"));
    }
  }
}

