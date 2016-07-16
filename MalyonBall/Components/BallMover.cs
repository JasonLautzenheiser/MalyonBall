using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace MalyonBall.Components
{
  public class BallMover: Component,IUpdatable
  {
    private float speed=800f;
    private float radius;
    Vector2 direction;
    private PaddleMover paddleMover;
    Sprite paddleSprite;


    public BallMover()
    {
      direction = new Vector2(0.707f, -0.707f);
    }

    public override void onAddedToEntity()
    {
      paddleMover = entity.scene.entities.findEntity("paddle").getComponent<PaddleMover>();
      paddleSprite = paddleMover.entity.getComponent<Sprite>();
      radius = entity.getComponent<Ball>().Width / 2f;
    }

    public void update()
    {
      entity.transform.position += direction*speed*Time.deltaTime;

      checkCollisions();
    }

    private void checkCollisions()
    {
      var paddlePosition = paddleMover.entity.transform.position;
      var ballPosition = entity.transform.position;

      if ((ballPosition.X > paddlePosition.X - radius - paddleSprite.width/2) &&
          (ballPosition.X < paddlePosition.X + radius + paddleSprite.width/2) &&
          (ballPosition.Y > paddlePosition.Y - radius - paddleSprite.height/2) &&
          (ballPosition.Y < paddlePosition.Y))
      {
        Vector2 normal = -1.0f*Vector2.UnitY;

        float dist = paddleSprite.width + radius*2;
        float ballLocation = ballPosition.X - (paddlePosition.X - radius - paddleSprite.width/2.0f);

        float pct = ballLocation/dist;

        if (pct < 0.33f)
          normal = new Vector2(-0.196f, -0.981f);
        else if (pct > 0.66f)
          normal = new Vector2(0.196f, -0.981f);

        direction = Vector2.Reflect(direction, normal);
      }




      if (Math.Abs(ballPosition.X) < radius)
      {
        direction.X = -direction.X;
      }
      else if (Math.Abs(ballPosition.X - Screen.width) < radius)
      {
        direction.X = -direction.X;
      }
      else if (Math.Abs(ballPosition.Y) < radius)
      {
        direction.Y = -direction.Y;
      }
      else if (ballPosition.Y - Screen.height > 0)
      {
        direction = new Vector2(0.707f, -0.707f);
        entity.transform.position = new Vector2(paddlePosition.X, paddlePosition.Y - paddleSprite.height/2.0f - radius);
      }
    }
  }
}