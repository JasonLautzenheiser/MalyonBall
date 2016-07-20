using System;
using System.Linq;
using MalyonBall.Entities.Blocks;
using MalyonBall.Entities.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Shapes;
using MonoGame.Extended.Sprites;

namespace MalyonBall.Entities.Player
{
  public class Ball : Entity
  {
    private Sprite sprite;
    private int collisionFudge =10 ;
    private BallTrailer trailer;

    public float Speed = 400f;
    public float Radius;
    public Vector2 Direction;
    public Vector2 Position
    {
      get { return sprite.Position; }
      set
      {
        sprite.Position = value;
        BoundingCircle.Center = value;
      }
    }
    public CircleF BoundingCircle;
    public Paddle paddle;


    public Vector2 Velocity { get; set; } = Vector2.Zero;

    public Ball(Vector2 direction)
    {
      init(direction);
    }

    public Ball()
    {
      init(new Vector2(0.707f, -0.707f));
    }

    private void init(Vector2 direction)
    {
      Direction = direction;
      sprite = new Sprite(Art.Ball);
      Radius = sprite.TextureRegion.Width / 2.0f;
      Position = new Vector2((GameCore.ViewPort.Width / 2.0f) - sprite.TextureRegion.Width / 2.0f, GameCore.ViewPort.Height - sprite.TextureRegion.Height - 10);
      trailer = (BallTrailer)EffectsManager.Add<BallTrailer>(Position,this);
      paddle = GameCore.Instance.EntityManager.FindEntity<Paddle>();
      BoundingCircle = new CircleF(sprite.Position, Radius);

    }

    public override void Update(GameTime gameTime)
    {
      Velocity = Direction*Speed;
      Position += Velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
      trailer.Trigger(Position);
      checkCollision();
    }

    private void checkCollision()
    {
      // colliding with paddle
      if (collisionFudge == 0)
      {
        if (paddle != null && !paddle.IsDestroyed && paddle.CollisionBounds.GetBoundingRectangle().Intersects(BoundingCircle.GetBoundingRectangle()))
        {
          collisionFudge = 20;
          Vector2 normal = -1.0f*Vector2.UnitY;

          float dist = paddle.Width + Radius*2.0f;
          float ballLocation = Position.X - (paddle.Position.X - Radius - paddle.Width/2.0f);

          float pct = ballLocation/dist;

          if (pct < 0.33f)
            normal = new Vector2(-0.196f, -0.981f);
          else if (pct > 0.66f)
            normal = new Vector2(0.196f, -0.981f);

          Direction = Vector2.Reflect(Direction, normal);

          float dotResult = Vector2.Dot(Direction, Vector2.UnitX);
          if (dotResult > 0.9f)
          {
            Vector3 crossResult = Vector3.Cross(new Vector3(Direction, 0), -Vector3.UnitY);
            Direction = crossResult.Z < 0 ? new Vector2(0.423f, -0.906f) : new Vector2(-0.423f, -0.906f);
          }
        }
      }
      else
      {
        collisionFudge--;
      }

      // colliding with walls
      if (Math.Abs(BoundingCircle.GetBoundingRectangle().X) < Radius)
      {
        Direction.X = -Direction.X;
      }
      else if (Math.Abs(BoundingCircle.GetBoundingRectangle().X - GameCore.ViewPort.Width) < Radius)
      {
        Direction.X = -Direction.X;
      }
      else if (Math.Abs(BoundingCircle.GetBoundingRectangle().Y) < Radius)
      {
        Direction.Y = -Direction.Y;
      }
      else if (BoundingCircle.GetBoundingRectangle().Y - GameCore.ViewPort.Height > 0)
      {
        Direction = new Vector2(0.707f, -0.707f);

        // ball went off bottom of screen...handle loss and restart.
        Position = new Vector2(paddle.Position.X, paddle.Position.Y - paddle.Height / 2.0f - Radius - 2.0f);
      }

      // colliding with brick
      foreach (var block in GameCore.Instance.EntityManager.Entities.OfType<Block>())
      {
        if (BoundingCircle.GetBoundingRectangle().Intersects(block.CollisionBounds))
        {
          Direction.Y = -Direction.Y;
          EffectsManager.AddAndTrigger<BlockDestruction>(Position);
          block.Destroy();
          break;
        }
      }


    }


    public override void Draw(SpriteBatch batch)
    {
      batch.DrawRectangle(BoundingCircle.GetBoundingRectangle(), Color.Red);
      batch.Draw(sprite);
    }
  }
}