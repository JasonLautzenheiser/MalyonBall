using System.CodeDom.Compiler;
using System.Media;
using MalyonBall.Entities;
using MalyonBall.Entities.Blocks;
using MalyonBall.Entities.Player;
using MalyonBall.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MalyonBall
{
  public class GameCore : Game
  {
    public static GameCore Instance { get; private set; }
    public static Viewport ViewPort => Instance.GraphicsDevice.Viewport;
    public static Vector2 ScreenSize => new Vector2(ViewPort.Width, ViewPort.Height);
    public static GameTime GameTime { get; private set; }
    public static LevelManager LevelManager { get; private set; }
    public static ParticleManager<ParticleState> ParticleManager { get; private set; }
    private readonly GraphicsDeviceManager graphicsDeviceManager;
    public readonly EntityManager EntityManager;
    private SpriteBatch spriteBatch;
    private TitleScreen splashScreen;
    private GameScreen gameScreen;


    public GameCore()
    {
      Instance = this;
      graphicsDeviceManager = new GraphicsDeviceManager(this);
      EntityManager = new EntityManager();
      LevelManager = new LevelManager();
      Content.RootDirectory = "Content";
      Window.AllowUserResizing = true;
      Window.Position = Point.Zero;
      IsMouseVisible = false;

      graphicsDeviceManager.PreferredBackBufferWidth = 1422;
      graphicsDeviceManager.PreferredBackBufferHeight = 900;

    }

    protected override void Initialize()
    {
      InputManager.Initialize();
      ParticleManager = new ParticleManager<ParticleState>(1024 * 20, ParticleState.UpdateParticle);

      base.Initialize();

    }

    protected override void LoadContent()
    {
      Font.Load(Content);
      Art.Load(Content);
      Sounds.Load(Content);

      gameScreen = new GameScreen()
      {
        Enabled = true,
        Visible = true
      };
      ScreenManager.Add(gameScreen);

      splashScreen = new TitleScreen()
      {
        Enabled = false,
        Visible = false
      };
      ScreenManager.Add(splashScreen);

      spriteBatch = new SpriteBatch(GraphicsDevice);

    }

    protected override void UnloadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
      GameTime = gameTime;
      InputManager.Update();

      if (InputManager.IsActionTriggered(InputManager.Action.ExitGame))
        Exit();

      ScreenManager.Update(gameTime);

      base.Update(gameTime);
    }



    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.Black);

      spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
      ScreenManager.Draw(spriteBatch);
      spriteBatch.End();

      spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
      GameCore.ParticleManager.Draw(spriteBatch);
      spriteBatch.End();




      base.Draw(gameTime);
    }
  }
}
