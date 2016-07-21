using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using MonoGame.Extended.Sprites;

namespace MalyonBall.Entities.Player
{
  public class Paddle : Entity
  {
    private readonly Sprite sprite;

    public int Width => sprite.TextureRegion.Width;
    public int  Height => sprite.TextureRegion.Height;
    public float Speed = 500f;
    public RectangleF CollisionBounds;
    public Vector2 Position
    {
      get { return sprite.Position; }
      set {
        sprite.Position = value;
        CollisionBounds.Location = value - new Vector2(Width/2.0f, Height/2.0f);
        CollisionBounds.Size = new SizeF(Width, Height);
      } 
    }

    public Paddle()
    {
      sprite = new Sprite(Art.Paddle);

      Position = new Vector2((GameCore.ViewPort.Width/2.0f) - sprite.TextureRegion.Width/2.0f, GameCore.ViewPort.Height - sprite.TextureRegion.Height - 10);

    }

    public override void Update(GameTime gameTime)
    {
      var width = sprite.TextureRegion.Width;

      MouseState mouseState = Mouse.GetState();

      Position = new Vector2(MathHelper.Clamp(mouseState.X, 0 + width / 2.0f, GameCore.ViewPort.Width - width / 2.0f), Position.Y);
    }

    public override void Draw(SpriteBatch batch)
    {
      if (GameState.dbgShowCollisionBounds)
        batch.DrawRectangle(CollisionBounds, Color.Red);

      batch.Draw(sprite);
    }
  }
}