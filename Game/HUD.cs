using SFML.Graphics;
using SFML.Window;

namespace TcGame
{
  public class HUD : Actor
  {
    private Text lifeText;
    private Text pointsText;
    private Text infoText;

    private bool showInfo;

    public int NumPoints { get; set; }

    public int NumLifes { get; set; }

    public bool ShowScore { get; set; }

    public void ShowInfo(string info)
    {
      infoText.DisplayedString = info;
      showInfo = true;
    }

    public void HideInfo()
    {
      showInfo = false;
    }

    public HUD()
    {
      lifeText = new Text("Lifes: 0", Resources.Font("Fonts/LuckiestGuy"));
      lifeText.Position = new Vector2f(20.0f, 0.0f);

      pointsText = new Text("Points: 0", Resources.Font("Fonts/LuckiestGuy"));
      pointsText.Position = new Vector2f(20.0f, 30.0f);

      infoText = new Text(" ", Resources.Font("Fonts/LuckiestGuy"));
      infoText.CharacterSize = 150;
    }

    public override void Draw(RenderTarget rt, RenderStates rs)
    {
      if (ShowScore)
      {
        lifeText.DisplayedString = "Lifes: " + NumLifes;
        pointsText.DisplayedString = "Points: " + NumPoints;

        rt.Draw(lifeText, rs);
        rt.Draw(pointsText, rs);
      }

      if (showInfo)
      {
        var screenSize = Engine.Get.Window.Size;
        infoText.Origin = new Vector2f(infoText.GetLocalBounds().Width, infoText.GetLocalBounds().Height) / 2.0f;
        infoText.Position = new Vector2f(screenSize.X / 2.0f, 300.0f);

        rt.Draw(infoText, rs);
      }
    }
  }
}
