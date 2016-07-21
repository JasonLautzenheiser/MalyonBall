using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace MalyonBall.Entities.Blocks
{
  public class GreenBlock : Block
  {
    public GreenBlock(Vector2 position) : base(position)
    {
    }

    public GreenBlock()
    {
    }


    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch batch)
    {
      batch.Draw(Sprite.TextureRegion, Sprite.Position, Color.Green);
      base.Draw(batch);
    }
  }
}