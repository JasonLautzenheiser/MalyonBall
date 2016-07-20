using System;
using MalyonBall.Entities.Player;
using MalyonBall.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace MalyonBall.Entities.Effects
{
  public class BallTrailer :Effect
  {
    public Ball Ball { get; set; }
    private Texture2D particleTexture;
    private FastRandom rand;

    public override void Init(Entity entity = null)
    {
      if (entity == null) return;
      Ball = (Ball) entity;

      rand = new FastRandom();

      particleTexture = Art.TextParticle;
    }

    public override void Trigger(Vector2 position)
    {
      
    }

    public override void Update(GameTime gameTime)
    {
      if (Ball.Velocity.LengthSquared() > 0.1f)
      {
        float rotation = Ball.Direction.ToAngle();
        Quaternion rot = Quaternion.CreateFromYawPitchRoll(0f, 0f, rotation);

        double t = GameCore.GameTime.TotalGameTime.TotalSeconds;
        Vector2 baseVel = Ball.Velocity.ScaleTo(1); 
        Vector2 perpVel = new Vector2(baseVel.Y, -baseVel.X) * (0.5f * (float)Math.Sin(t * 10));
        Color sideColor = new Color(200, 38, 9);
        Color midColor = new Color(255, 187, 39);
        Vector2 pos = Ball.Position + Vector2.Transform(new Vector2(0, 0), rot);
        const float ALPHA = 0.7f;

        Vector2 velMid = baseVel + rand.NextVector2(0,1);
        GameCore.ParticleManager.CreateParticle(particleTexture, pos, Color.White * ALPHA, 60f, new Vector2(0.5f, 1), new ParticleState(velMid, ParticleType.ShipExhaust));
        GameCore.ParticleManager.CreateParticle(particleTexture, pos, midColor * ALPHA, 60f, new Vector2(0.5f, 1), new ParticleState(velMid, ParticleType.ShipExhaust));

        // side particle streams
        Vector2 vel1 = baseVel + perpVel + rand.NextVector2(0,0.3f);
        Vector2 vel2 = baseVel - perpVel + rand.NextVector2(0, 0.3f);

        GameCore.ParticleManager.CreateParticle(particleTexture, pos, Color.White * ALPHA, 60f, new Vector2(0.5f, 1), new ParticleState(vel1, ParticleType.ShipExhaust));
        GameCore.ParticleManager.CreateParticle(particleTexture, pos, Color.White * ALPHA, 60f, new Vector2(0.5f, 1), new ParticleState(vel2, ParticleType.ShipExhaust));

        GameCore.ParticleManager.CreateParticle(particleTexture, pos, sideColor * ALPHA, 60f, new Vector2(0.5f, 1), new ParticleState(vel1, ParticleType.ShipExhaust));
        GameCore.ParticleManager.CreateParticle(particleTexture, pos, sideColor * ALPHA, 60f, new Vector2(0.5f, 1), new ParticleState(vel2, ParticleType.ShipExhaust));

      }
    }

    public override void Draw(SpriteBatch batch)
    {
      
    }
  }
}