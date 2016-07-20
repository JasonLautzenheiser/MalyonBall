using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MalyonBall
{
  public static class ScreenManager
  {
    private static readonly List<Screen> screens;

    static ScreenManager()
    {
      screens = new List<Screen>();
    }

    public static void Update(GameTime gameTime)
    {
      foreach (var screen in screens.Where(p=>p.Enabled))
      {
        screen.Update(gameTime);
      }
    }

    public static void Draw(SpriteBatch batch)
    {
      foreach (var screen in screens.Where(p => p.Enabled))
      {
        screen.Draw(batch);
      }
    }

    public static void Add(Screen screen)
    {
      screens.Add(screen);
    }
  }
}