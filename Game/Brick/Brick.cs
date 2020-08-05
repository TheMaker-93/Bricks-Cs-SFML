using SFML.Graphics;
using SFML.Window;

namespace TcGame
{
  public class Brick : StaticActor
  {
    /// <summary>
    /// Material of the brick
    /// </summary>
    private BrickMaterial material;

    /// <summary>
    /// Gets or sets the material.
    /// </summary>
    public BrickMaterial Material
    {
      get
      {
        return material;
      }
      set
      {
        material = value;
        Sprite = new Sprite(broken ? material.BrokenTexture : material.NormalTexture);
        Center();
      }
    }

    private bool broken = false;

    /// <summary>
    /// Checks if it collides with ball and adjust ball forward and position
    /// </summary>
    public bool CheckBallCollision(Ball ball)
    {
      bool collision = false;

      FloatRect ballBounds = ball.GetGlobalBounds();
      FloatRect myBounds = GetGlobalBounds();

      Vector2f top = new Vector2f(ballBounds.Left + ballBounds.Width / 2.0f, ballBounds.Top);
      Vector2f bottom = new Vector2f(ballBounds.Left + ballBounds.Width / 2.0f, ballBounds.Top + ballBounds.Height);
      Vector2f left = new Vector2f(ballBounds.Left, ballBounds.Top + ballBounds.Height / 2.0f);
      Vector2f right = new Vector2f(ballBounds.Left + ballBounds.Width, ballBounds.Top + ballBounds.Height / 2.0f);

      if (myBounds.Contains(top.X, top.Y))
      {
        // Brick bottom
        collision = true;
        ball.Forward = new Vector2f(ball.Forward.X, -ball.Forward.Y);
        ball.Position = new Vector2f(ball.Position.X, myBounds.Top + myBounds.Height + ballBounds.Height / 2.0f);
      }
      else if (myBounds.Contains(bottom.X, bottom.Y))
      {
        // Brick top
        collision = true;
        ball.Forward = new Vector2f(ball.Forward.X, -ball.Forward.Y);
        ball.Position = new Vector2f(ball.Position.X, myBounds.Top - ballBounds.Height / 2.0f);
      }
      else if (myBounds.Contains(right.X, right.Y))
      {
        // Brick left
        collision = true;
        ball.Forward = new Vector2f(-ball.Forward.X, ball.Forward.Y);
        ball.Position = new Vector2f(myBounds.Left - ballBounds.Width / 2.0f, ball.Position.Y);
      }
      else if (myBounds.Contains(left.X, left.Y))
      {
        // Brick right
        collision = true;
        ball.Forward = new Vector2f(-ball.Forward.X, ball.Forward.Y);
        ball.Position = new Vector2f(myBounds.Left + myBounds.Width + ballBounds.Width / 2.0f, ball.Position.Y);
      }

      if (collision && material.CanBeDestroyed)
      {
        if (!broken && material.CanBeBroken)
        {
          broken = true;
          Sprite = new Sprite(material.BrokenTexture);
          Center();
        }
        else
        {
          Destroy();
        }
      }

      return collision;
    }
  }
}
