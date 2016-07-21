using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;


namespace MalyonBall.Entities.Blocks
{
  public class RedBlock : Block
  {
    public RedBlock(Vector2 position) : base(position)
    {
    }

    public RedBlock()
    {
    }

    public override void Update(GameTime gameTime)
    {

    }

    public override void Draw(SpriteBatch batch)
    {
      batch.Draw(Sprite.TextureRegion, Sprite.Position, Color.Red);
      base.Draw(batch);
      
    }
  }
}