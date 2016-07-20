using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace MalyonBall
{
  public abstract class Screen
  {
    public abstract void Draw(SpriteBatch batch);
    public abstract void Update(GameTime gameTime);
    public bool Visible { get; set; }
    public bool Enabled { get; set; }
  }
}