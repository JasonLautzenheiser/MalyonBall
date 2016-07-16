using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace MalyonBall.Components
{
  public class PaddleMover :Component,IUpdatable
  {
    public float speed = 500f;

    public void update()
    {
      var moveDir = Vector2.Zero;

      if (Input.isKeyDown(Keys.Left))
        moveDir.X = -1f;
      else if (Input.isKeyDown(Keys.Right))
        moveDir.X = 1f;

      var width = entity.getComponent<Sprite>().width;


      entity.transform.position += moveDir*speed*Time.deltaTime;
      entity.transform.position = new Vector2(MathHelper.Clamp(entity.transform.position.X, 0+width/2.0f, Screen.width - width/2.0f), entity.transform.position.Y);
    }
  }
}