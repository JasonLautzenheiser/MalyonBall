using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace MalyonBall.Entities.Blocks
{
  public enum BlockType
  {
    None, 
    Green, 
    Red
  }

  public abstract class Block : Entity
  {
    protected Block()
    {
      Sprite = new Sprite(Art.Block);
    }

    protected Block(Vector2 position)
    {
      Sprite = new Sprite(Art.Block);
      Position = position;

    }

    public RectangleF CollisionBounds;
    public Vector2 Position
    {
      get { return Sprite.Position; }
      set
      {
        Sprite.Position = value;
        CollisionBounds.Location = value;
        CollisionBounds.Size = new SizeF(Width, Height);
      }
    }
    protected readonly Sprite Sprite;
    public int Width => Sprite.TextureRegion.Width;
    public int Height => Sprite.TextureRegion.Height;

    public override void Draw(SpriteBatch batch)
    {
      if (GameState.dbgShowCollisionBounds)
        batch.DrawRectangle(CollisionBounds.GetBoundingRectangle(), Color.LimeGreen);
    }
  }
}