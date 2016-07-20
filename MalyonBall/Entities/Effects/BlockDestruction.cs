using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.TextureAtlases;

namespace MalyonBall.Entities.Effects
{
  public class BlockDestruction : Effect
  {
    private ParticleEffect particleEffect;

    public override void Init(Entity entity = null)
    {
      var particleTexture = new Texture2D(GameCore.Instance.GraphicsDevice, 1, 1);
      particleTexture.SetData(new[] {Color.White});
      var textureRegion = new TextureRegion2D(particleTexture);

      particleEffect = new ParticleEffect
      {
        Emitters = new[]
        {
          new ParticleEmitter(1000, TimeSpan.FromSeconds(0.75), Profile.Point())
          {
            TextureRegion = textureRegion,
            Parameters = new ParticleReleaseParameters
            {
              Speed = new Range<float>(0f, 250f),
              Quantity = 500,
              Rotation = new Range<float>(0f, 0f),
              Scale = new Range<float>(1.0f, 1.0f)
            },
            Modifiers = new IModifier[]
            {
              new AgeModifier
              {
                Interpolators = new IInterpolator[]
                {
                  new OpacityInterpolator {StartValue = 1.0f, EndValue = 0.0f}, 
                  new ColorInterpolator {InitialColor = new HslColor(1f, .8784314f, .7529412f), FinalColor = new HslColor(1f, .8784314f, .7529412f)}
                }
              },
            }
          }
        }
      };
      return;
    }

    public override void Trigger(Vector2 position)
    {
      particleEffect.Trigger(position);
    }



    public override void Update(GameTime gameTime)
    {
      particleEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
    }

    public override void Draw(SpriteBatch batch)
    {
      batch.Draw(particleEffect);
    }
  }
}