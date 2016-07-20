using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.BitmapFonts;

namespace MalyonBall
{
  public static class Font
  {
    public static BitmapFont MainFont { get; private set; }
    public static BitmapFont TitleFont { get; private set; }

    public static void Load(ContentManager content)
    {
      TitleFont = content.Load<BitmapFont>(@"fonts\TitleFont");
    }

  }
}