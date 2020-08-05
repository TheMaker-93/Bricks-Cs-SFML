using System.Collections.Generic;
using System;
using SFML.Window;

namespace TcGame
{
  /// <summary>
  /// Contains all the bricks
  /// </summary>
  public class BrickWall : Actor
  {
    /// <summary>
    /// Helper method for create a new BrickMaterial
    /// </summary>
    private static BrickMaterial NewMaterial(string color, bool canBeDestroyed, bool canBeBroken, Type giftType)
    {
      return new BrickMaterial
      {
        NormalTexture = Resources.Texture("Arkanoid/Textures/Bricks/brick_" + color),
        BrokenTexture = Resources.Texture("Arkanoid/Textures/Bricks/brick_" + color + "_broken"),
        CanBeBroken = canBeBroken,
        CanBeDestroyed = canBeDestroyed,
        GiftType = giftType
      };
    }

    /// <summary>
    /// Fill the wall with bricks
    /// </summary>
    public void ConstructWall(int numRows, int numColumns)
    {
      BrickMaterial[] brickMaterials =
        {
          NewMaterial("blue", true, true, typeof(GiftBall)),
          NewMaterial("gray", false, false, null),
          NewMaterial("green", true, true, typeof(GiftBall)),
          NewMaterial("yellow", true, true, typeof(GiftBomb)),
          NewMaterial("pink", true, false, typeof(GiftHeart)),
          NewMaterial("violet", true, false, typeof(GiftCoin))
        };

      Random r = Engine.Get.random;

      for (int row = 0; row < numRows; ++row)
      {
        for (int column = 0; column < numColumns; ++column)
        {
          Brick brick = Engine.Get.Scene.Create<Brick>(this);
          brick.Material = brickMaterials[r.Next(brickMaterials.Length)];
          brick.Position = new Vector2f(column * brick.GetLocalBounds().Width, row * brick.GetLocalBounds().Height);
        }
      }
    }

    /// <summary>
    /// Ball collision tests
    /// </summary>
    public void CheckBallCollision(Ball ball)
    {
      var bricks = Engine.Get.Scene.GetAll<Brick>();

      foreach (var brick in bricks)
      {
        if (brick.CheckBallCollision(ball))
        {
          break;
        }
      }
    }
  }
}
