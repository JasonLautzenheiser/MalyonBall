using MalyonBall.Entities.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace MalyonBall.Entities.Blocks
{
  public class Block : Entity
  {
    private Sprite sprite;
    public int Width => sprite.TextureRegion.Width;
    public int Height => sprite.TextureRegion.Height;
    public RectangleF CollisionBounds;
    public Vector2 Position
    {
      get { return sprite.Position; }
      set
      {
        sprite.Position = value;
        CollisionBounds.Location = value;
        CollisionBounds.Size = new SizeF(Width, Height);
      }
    }


    public Block(Vector2 position)
    {
      sprite = new Sprite(Art.Block);
      Position = position;

    }


    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch batch)
    {
      batch.Draw(sprite.TextureRegion, sprite.Position, Color.Green);
      batch.DrawRectangle(CollisionBounds.GetBoundingRectangle(), Color.LimeGreen);
    }
  }
}