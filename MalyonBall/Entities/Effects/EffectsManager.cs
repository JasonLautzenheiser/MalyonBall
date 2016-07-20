using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MalyonBall.Entities.Effects
{
  public static class EffectsManager
  {
    public static Effect Add<T>(Vector2 position, Entity entity = null) where T : Effect
    {
      var effect = (Effect)Activator.CreateInstance(typeof(T));
      effect.Init(entity);

      GameCore.Instance.EntityManager.AddEntity((Entity)effect);

      return effect;
    }

    public static void AddAndTrigger<T>(Vector2 position, Entity entity = null) where T : Effect
    {
      var effect = Add<T>(position, entity);
      effect.Trigger(position);

    }


  }
}