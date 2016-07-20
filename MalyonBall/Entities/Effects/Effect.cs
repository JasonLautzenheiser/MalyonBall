using Microsoft.Xna.Framework;
using MonoGame.Extended.Particles;

namespace MalyonBall.Entities.Effects
{
  public abstract class Effect : Entity
  {
    public virtual void Init(Entity entity = null)
    {
      return;
    }

    public virtual void Trigger(Vector2 position)
    {
    }
  }
}