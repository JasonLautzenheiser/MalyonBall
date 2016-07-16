using MalyonBall.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace MalyonBall.Scenes
{
  public class MalyonGameBoard : Scene
  {

    public MalyonGameBoard()
    {
    }

    public override void initialize()
    {
      clearColor = Color.Black;
      addRenderer(new DefaultRenderer());

      var paddleTexture = contentManager.Load<Texture2D>(@"textures\paddle20020");
      var paddle = createEntity("paddle");
      paddle.transform.setPosition((Screen.width / 2.0f) - paddleTexture.Width / 2.0f, Screen.height - paddleTexture.Height - 10);
      paddle.addComponent(new Sprite(paddleTexture));
      paddle.addComponent(new PaddleMover());

      var ballTexture = contentManager.Load<Texture2D>(@"textures\GreenBall");
      var ball = createEntity("ball");
      ball.transform.setPosition((Screen.width / 2.0f) - ballTexture.Width / 2.0f, Screen.height - ballTexture.Height - 30);
      ball.addComponent(new Ball(ballTexture));
      ball.addComponent(new Sprite(ballTexture));
      ball.addComponent(new BallMover());



      base.initialize();
    }
  }
}