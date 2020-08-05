using SFML.Graphics;
using SFML.Window;

namespace TcGame
{
  public class Ball : StaticActor
  {
    public float Speed = 800.0f;

    public Vector2f Forward { get; set; }

    private bool locked;    // se puede o no se pued emover
    private bool stored;    // marca si la bola esta guardada en la pala o no

    public Ball()
    {
      Sprite = new Sprite(Resources.Texture("Arkanoid/Textures/Balls/ball_green"));
      Center();
      Forward = new Vector2f(0.0f, -1.0f);

      locked = true;    // la bola empieza bloqueada 
    }

    public override void Update(float dt)
    {
      if (locked == false) {
        Position += Forward.Normal () * Speed * dt;
        CheckCollisions ();
      } 

    }

    private void CheckCollisions()
    {
      var wall = Engine.Get.Scene.GetFirst<BrickWall>();
      if (wall != null)
      {
        wall.CheckBallCollision(this);
      }

      CheckPadCollision();
      CheckWallCollision();
    }

    private void CheckPadCollision()
    {
      if (Speed > 0.0f)
      {
        FloatRect myBounds = GetGlobalBounds();

        var pad = Engine.Get.Scene.GetFirst<Pad>();
        if (pad != null)
        {
          if (myBounds.Intersects(pad.GetGlobalBounds()))
          {
            float x = myBounds.Left + myBounds.Width / 2.0f;
            float padX = pad.Position.X;

            float d = (x - padX) / pad.GetLocalBounds().Width;
            Forward = (new Vector2f(0.0f, -1.0f)).Rotate(90.0f * d);
            Position = new Vector2f(Position.X, pad.GetGlobalBounds().Top - pad.GetLocalBounds().Height / 2.0f);
          }
        }
      }
    }

    private void CheckWallCollision()
    {
      FloatRect myBounds = GetGlobalBounds();

      Vector2u screenBounds = Engine.Get.Window.Size;

      if (myBounds.Left <= 0.0f)
      {
        Forward = new Vector2f(-Forward.X, Forward.Y);
        Position = new Vector2f(myBounds.Width * 0.5f, Position.Y);
      }
      else if ((myBounds.Left + myBounds.Width * 0.5f) >= screenBounds.X)
      {
        Forward = new Vector2f(-Forward.X, Forward.Y);
        Position = new Vector2f(screenBounds.X - myBounds.Width * 0.5f, Position.Y);
      }
      else if (myBounds.Top <= 0.0f)
      {
        Forward = new Vector2f(Forward.X, -Forward.Y);
        Position = new Vector2f(Position.X, myBounds.Height * 0.5f);
      }
      else if (myBounds.Top >= screenBounds.Y)
      {
        Destroy();
      }
    }

    public void setLock (bool lockStatus)
    {
      locked = lockStatus;
    }
    public bool GetLock ()
    {
      return locked;
    }





  }
}
