using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace MalyonBall.Components
{
  public class Ball : Component
  {
    readonly Texture2D ballTexture;

    public Ball(Texture2D texture)
    {
      ballTexture = texture;
    }

    public float Width => ballTexture.Width;
    public float Height => ballTexture.Height;
  }
}