using System;
using System.Linq;
using MalyonBall.Entities.Blocks;
using MalyonBall.Entities.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using MonoGame.Extended.Sprites;

namespace MalyonBall.Entities.Player
{
  public enum BallMovementState
  {
    OnPaddle,
    Captured,
    Moving
  }

  public class Ball : Entity
  {
    private Sprite sprite;
    private int collisionFudge =10 ;
    private BallTrailer trailer;
    private BallMovementState movementState = BallMovementState.Captured;

    private const float speedYIncrease = 1.25f;
    private const float speedXIncrease = 1.25f;


    public float Speed = 400f;
    public float Radius;
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
    private FastRandom r;


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
      movementState = BallMovementState.OnPaddle;
      sprite = new Sprite(Art.Ball);
      Radius = sprite.TextureRegion.Width / 2.0f;
      paddle = GameCore.Instance.EntityManager.FindEntity<Paddle>();
      BoundingCircle = new CircleF(sprite.Position, Radius);

      if (movementState == BallMovementState.OnPaddle)
        Position = paddle.Position + new Vector2(0, -paddle.Height / 2.0f);
      else
        Position = new Vector2((GameCore.ViewPort.Width / 2.0f) - sprite.TextureRegion.Width / 2.0f, GameCore.ViewPort.Height - sprite.TextureRegion.Height - 10);

      trailer = (BallTrailer)EffectsManager.Add<BallTrailer>(Position,this);
      r = new FastRandom();
    }

    public override void Update(GameTime gameTime)
    {
      if (movementState == BallMovementState.OnPaddle)
      {
        updateOnPaddle();
        if (InputManager.IsActionTriggered(InputManager.Action.LaunchBall) || Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
          fireFromPaddle();
        }
      }
      else
      {
        updateMoving(gameTime);
      }
    }

    private void updateMoving(GameTime gameTime)
    {
      Position += Velocity*(float) gameTime.ElapsedGameTime.TotalSeconds;
      trailer.Trigger(Position);
      checkCollision();
    }

    private void fireFromPaddle()
    {
      movementState = BallMovementState.Moving;

      float randomXSpeed = r.NextSingle(400f, 500f);
      float randomXDir = r.NextSingle(-0.7f, 0.7f);
      Velocity = new Vector2(Math.Abs(randomXDir) < 0.01f ? 0.01f : randomXDir, -1)* randomXSpeed;
    }

    private void updateOnPaddle()
    {
      Position = paddle.Position + new Vector2(0, -paddle.Height);
    }

    private void checkCollision()
    {
      // colliding with walls
      handleBorderCollisions();

      // colliding with paddle
      if (collisionFudge == 0)
      {
        isInAreaOfPaddle();
      }
      else
      {
        collisionFudge--;
      }

      // colliding with brick
      handleBrickCollisions();

      handleLostBall();

    }

    private void handleBrickCollisions()
    {
      foreach (var block in GameCore.Instance.EntityManager.Entities.OfType<Block>())
      {
        if (BoundingCircle.GetBoundingRectangle().Intersects(block.CollisionBounds))
        {
          Velocity = new Vector2(Velocity.X, -Velocity.Y);
          EffectsManager.AddAndTrigger<BlockDestruction>(Position);
          block.Destroy();
          break;
        }
      }
    }

    private void handleLostBall()
    {
      if (BoundingCircle.GetBoundingRectangle().Y - GameCore.ViewPort.Height > 0)
      {
        Velocity = Vector2.Zero;

        // ball went off bottom of screen...handle loss and restart.
        Position = new Vector2(paddle.Position.X, paddle.Position.Y - paddle.Height/2.0f - Radius - 2.0f);
        movementState = BallMovementState.OnPaddle;
      }
    }

    private void handleBorderCollisions()
    {
      if (Math.Abs(BoundingCircle.GetBoundingRectangle().X) < Radius)
      {
        Velocity = new Vector2(-Velocity.X,Velocity.Y);
      }
      else if (Math.Abs(BoundingCircle.GetBoundingRectangle().X - GameCore.ViewPort.Width) < Radius)
      {
        Velocity = new Vector2(-Velocity.X, Velocity.Y);
      }
      else if (Math.Abs(BoundingCircle.GetBoundingRectangle().Y) < Radius)
      {
        Velocity = new Vector2(Velocity.X, -Velocity.Y);
      }
    }

    private void isInAreaOfPaddle()
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

        Velocity = Vector2.Reflect(Velocity, normal);
        float speed = Velocity.Length();

        float dotResult = Vector2.Dot(Velocity, Vector2.UnitX);
        if (dotResult > 0.9f)
        {
          Vector3 crossResult = Vector3.Cross(new Vector3(Velocity, 0), -Vector3.UnitY);
          Velocity = (crossResult.Z < 0 ? new Vector2(0.423f, -0.906f) : new Vector2(-0.423f, -0.906f)) * speed;
        }
        Velocity =  new Vector2(MathHelper.Clamp(Velocity.X + (Position.X - paddle.Position.X) * speedXIncrease,-700, 700), MathHelper.Clamp(-Math.Abs(Velocity.Y) * speedYIncrease,-700, 700));
        
      }
    }

    public override void Draw(SpriteBatch batch)
    {
      batch.DrawRectangle(BoundingCircle.GetBoundingRectangle(), Color.Red);
      batch.Draw(sprite);
    }
  }
}