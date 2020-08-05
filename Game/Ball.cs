using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using System;

namespace TcGame
{
  public class Ball : StaticActor
  {
    public float Speed = 800.0f;

    public Vector2f Forward { get; set; }

    private bool locked;    // se puede o no se pued emover
    private bool stored;    // marca si la bola esta guardada en la pala o no

    SoundBuffer sfBall_01 = new SoundBuffer ("Data/Arkanoid/Sounds/ball01.wav");
    SoundBuffer sfBall_02 = new SoundBuffer ("Data/Arkanoid/Sounds/ball02.wav");
    SoundBuffer sfBall_03 = new SoundBuffer ("Data/Arkanoid/Sounds/ball03.wav");
    Sound ballSFX;

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
      bool padCollisioned = false;

      if (Speed > 0.0f)
      {
        FloatRect myBounds = GetGlobalBounds();

        var pad = Engine.Get.Scene.GetFirst<Pad>();
        if (pad != null)
        {
          if (myBounds.Intersects(pad.GetGlobalBounds()))
          {

            padCollisioned = true;
            float x = myBounds.Left + myBounds.Width / 2.0f;
            float padX = pad.Position.X;

            float d = (x - padX) / pad.GetLocalBounds().Width;
            Forward = (new Vector2f(0.0f, -1.0f)).Rotate(90.0f * d);
            Position = new Vector2f(Position.X, pad.GetGlobalBounds().Top - pad.GetLocalBounds().Height / 2.0f);
          }
        }
      }

      // SONIDOS DE LA PELOTA CON LAS PAREDES
      if (padCollisioned == true) {
        Random alea = new Random ();
        switch (alea.Next (1, 4)) {
        case 1:
          ballSFX = new Sound (sfBall_01);
          break;
        case 2:
          ballSFX = new Sound (sfBall_02);
          break;
        case 3:
          ballSFX = new Sound (sfBall_03);
          break;
        }
        ballSFX.Play ();
      }

    }

    private void CheckWallCollision()
    {
      bool ballWallColision = false;
      FloatRect myBounds = GetGlobalBounds();

      Vector2u screenBounds = Engine.Get.Window.Size;

      if (myBounds.Left <= 0.0f)
      {
        Forward = new Vector2f(-Forward.X, Forward.Y);
        Position = new Vector2f(myBounds.Width * 0.5f, Position.Y);
        ballWallColision = true;
      }
      else if ((myBounds.Left + myBounds.Width * 0.5f) >= screenBounds.X)
      {
        Forward = new Vector2f(-Forward.X, Forward.Y);
        Position = new Vector2f(screenBounds.X - myBounds.Width * 0.5f, Position.Y);
        ballWallColision = true;
      }
      else if (myBounds.Top <= 0.0f)
      {
        Forward = new Vector2f(Forward.X, -Forward.Y);
        Position = new Vector2f(Position.X, myBounds.Height * 0.5f);
        ballWallColision = true;
      }
      else if (myBounds.Top >= screenBounds.Y)
      {
        var pad = Engine.Get.Scene.GetFirst<Pad> ();
        if (pad.ballList.Count == 1) {
          MyGame.Get.HUD.NumLifes -= 1;   // restamos vidas
          // CAMBIAR EL ESTADO A WAITTING FOR BALL
          MyGame.Get.ChangeStateToWaitingForBall ();
        }


        Engine.Get.Scene.GetAll<Ball> ().Remove(this);    // la borramos de la lista de la pla
        pad.ballList.Remove (this);                       // la borramos de la lita del juego

        Destroy();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine ("Eliminamos bola");
        Console.ResetColor ();

      }

      // SONIDOS DE LA PELOTA CON LAS PAREDES
      if (ballWallColision == true) {
        Random alea = new Random ();
        switch (alea.Next (1, 4)) {
        case 1:
          ballSFX = new Sound (sfBall_01);
          break;
        case 2:
          ballSFX = new Sound (sfBall_02);
          break;
        case 3:
          ballSFX = new Sound (sfBall_03);
          break;
        }
        ballSFX.Play ();
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
