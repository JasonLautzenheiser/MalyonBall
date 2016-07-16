using MalyonBall.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace MalyonBall
{
  public class GameCore : Core
  {

    public GameCore() : base(width:1024, height:768,isFullScreen:false,enableEntitySystems:false)
    {
      Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
      base.Initialize();
      Window.AllowUserResizing = true;

      var malyonBoard = new MalyonGameBoard();

      scene = malyonBoard;
    }

    protected override void LoadContent()
    {
    }

    protected override void UnloadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      // TODO: Add your update logic here

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {

      base.Draw(gameTime);
    }
  }
}
