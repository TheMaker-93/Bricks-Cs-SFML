using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using System;

namespace TcGame
{
  public class Pad : StaticActor
  {
    public bool CanMove { get; set; }

    private const float speed = 1100f;   // velocidad de movimiento
    public List<Ball> ballList = new List<Ball>();   // lista de bolas local de la pala

    private Vector2f ballSpawnVerticalOffest = new Vector2f (0, 25);

    public Pad()
    {
      CanMove = false;
      Sprite = new Sprite(Resources.Texture("Arkanoid/Textures/Bats/bat_blue"));
      Center();
    }

    public Ball AddBall()
    {
      //TODO: Exercise 2

      // creasmos una bola en la escena
      Ball b = Engine.Get.Scene.Create<Ball> ();
      b.Position = Position - ballSpawnVerticalOffest;
      b.setLock (true);     // la pelota aparece bloqueada

      // lo guardamos en la lista de pad
      ballList.Add (b);

      return b;
    }

    public void LaunchBalls()
    {
      //TODO: Exercise 2
      if (ballList.Count > 0) {
        
        foreach (Ball b in ballList) {
          if (b.GetLock() == true) {
            b.setLock (false);
          }
        }

      }

    }

    public void Reset()
    {
      var window = Engine.Get.Window;
      Position = new Vector2f(window.Size.X / 2.0f, window.Size.Y - 100.0f);
    }

    public override void Update(float dt)
    {
      if (CanMove)
      {
        if (Keyboard.IsKeyPressed (Keyboard.Key.Right)) {

          this.Position = new Vector2f (Position.X + speed * dt, Position.Y );

        } else if (Keyboard.IsKeyPressed (Keyboard.Key.Left)) {

          this.Position = new Vector2f (Position.X - speed * dt, Position.Y );
        }

      }

      // si pulsamos espacio lanzamos una bola
      if (Keyboard.IsKeyPressed (Keyboard.Key.Space)) {
        LaunchBalls ();
      }

      // mantenemos la bola enganchada a la pala mientras este bloqueada
      if (ballList.Count > 0) {
        foreach (Ball b in ballList) {
          if (b.GetLock () == true) {
            b.Position = new Vector2f (Position.X, Position.Y - ballSpawnVerticalOffest.Y);
          }
        }

      }

    }
  }
}

