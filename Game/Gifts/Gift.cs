namespace TcGame
{
  public class Gift : StaticActor
  {
    public override void Update(float dt)
    {
      // Check if collides with pad
      CheckPadCollision();
    }

    private void CheckPadCollision()
    {
      var pad = Engine.Get.Scene.GetFirst<Pad>();
      if (pad != null)
      {
        if (pad.GetGlobalBounds().Contains(WorldPosition.X, WorldPosition.Y))
        {
          GiveGift();
          Destroy();
        }
      }
    }

    /// <summary>
    /// Executes the gift action when the pad takes it
    /// </summary>
    public virtual void GiveGift()
    {

    }
  }
}

