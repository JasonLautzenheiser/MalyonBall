using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace MalyonBall
{
  public static class Sounds
  {
    public static SoundEffect PaddleBounce { get; private set; }

    public static void Load(ContentManager content)
    {
      PaddleBounce = content.Load<SoundEffect>(@"sounds\paddlebounce");
    }
  }
}