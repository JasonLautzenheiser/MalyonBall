using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace MalyonBall.Entities
{
  public interface IEntityManager
  {
    T AddEntity<T>(T entity) where T : Entity;
  }

  public class EntityManager : IEntityManager, IUpdate
  {
    private readonly List<Entity> entities;
    public IEnumerable<Entity> Entities => entities;
    private IList<Entity> addedEntities = new List<Entity>();
    private static bool isUpdating;

    public EntityManager()
    {
      entities = new List<Entity>();
    }

    public T FindEntity<T>() where T : Entity
    {
      return entities.OfType<T>().FirstOrDefault();
    }

    public T AddEntity<T>(T entity) where T : Entity
    {
      if (!isUpdating)
        addEntity(entity);
      else
        addedEntities.Add(entity);

      return entity;
    }

    private Entity addEntity(Entity entity) 
    {
      entities.Add(entity);
      Debug.WriteLine($"Entities: {GameCore.Instance.EntityManager.Entities.Count()}");
      return entity;
    }

    public void Update(GameTime gameTime)
    {
      isUpdating = true;
      foreach (var entity in entities)
      {
        entity.Update(gameTime);
      }
      isUpdating = false;
      foreach (var addedEntity in addedEntities) { addEntity(addedEntity); }
      addedEntities.Clear();

      entities.RemoveAll(e => e.IsDestroyed);
    }

    public void Draw(SpriteBatch batch)
    {
      foreach (var entity in entities.Where(e=>!e.IsDestroyed))
      {
        entity.Draw(batch);
      }
    }

  }
}