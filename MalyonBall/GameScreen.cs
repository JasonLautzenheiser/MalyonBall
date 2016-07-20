using MalyonBall.Entities;
using MalyonBall.Entities.Blocks;
using MalyonBall.Entities.Player;
using MalyonBall.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MalyonBall
{
  public class GameScreen : Screen
  {

    private Paddle player;

    public GameScreen()
    {
      player = GameCore.Instance.EntityManager.AddEntity(new Paddle());
      GameCore.Instance.EntityManager.AddEntity(new Ball());
//      GameCore.Instance.EntityManager.AddEntity(new Ball(new Vector2(0.404f, -0.494f)));


      // load starting level

      for (int i = 0; i < 5; i++)
      {
        GameCore.Instance.EntityManager.AddEntity(new Block(new Vector2(64 * i + 50, 100)));
      }

    }

    public override void Draw(SpriteBatch batch)
    {
      // entities
      GameCore.Instance.EntityManager.Draw(batch);
    }

    public override void Update(GameTime gameTime)
    {
      GameCore.Instance.EntityManager.Update(gameTime);
      GameCore.ParticleManager.Update();

    }
  }
}