using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MalyonBall
{
  public static class Art
  {
    public static Texture2D Ball { get; private set; }
    public static Texture2D Paddle { get; private set; }
    public static Texture2D RedBlock { get; private set; }
    public static Texture2D Block { get; private set; }
    public static Texture2D TextParticle { get; set; }


    public static void Load(ContentManager content)
    {
      Ball = content.Load<Texture2D>(@"textures\GreenBall");
      Paddle = content.Load<Texture2D>(@"textures\paddle20020");
      RedBlock = content.Load<Texture2D>(@"textures\RedBlock");
      Block = content.Load<Texture2D>(@"textures\Block");
      TextParticle = content.Load<Texture2D>(@"textures\textparticle");
    }
  }
}