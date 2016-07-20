using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;

namespace MalyonBall
{
  public class TitleScreen : Screen
  {

    List<SimpleParticle> titleParticles;
    List<SimpleParticle> startParticles;
    Texture2D particleTexture;
    private float titleScale = 3.5f;
    private float keyToStartScale = 1.5f;
    private float timeFromStart = 0.0f;
    private FastRandom r;
    private Vector2 screenSize;

    public TitleScreen() 
    {
      particleTexture = Art.TextParticle;
      r = new FastRandom();

      // get title text
      screenSize = new Vector2(GameCore.ViewPort.Width, GameCore.ViewPort.Height);

      var textSize = Font.TitleFont.MeasureString("Malyon Ball");
      var titlePoints = GetParticlePositions(GameCore.Instance.GraphicsDevice, Font.TitleFont, "Malyon Ball");
      var startx = (screenSize.X/titleScale - textSize.Width);
      Vector2 offset = new Vector2(100, 100);  // (float) (screenSize.Y / keyToStartScale * 0.25)
      titleParticles = getStartingPositions(titlePoints,  offset);

      // get start button text
      var textSize2 = Font.TitleFont.MeasureString("Press any key to start.");
      var startPoints = GetParticlePositions(GameCore.Instance.GraphicsDevice, Font.TitleFont, "Press any key to start.");
//      Vector2 offset2 = new Vector2(screenSize.X/keyToStartScale - textSize2.Width, offset.Y + textSize2.Height + 200);
      Vector2 offset2 = new Vector2(100,200);
      startParticles = getStartingPositions(startPoints, offset2);
    }

    private List<SimpleParticle> getStartingPositions(List<Vector2> titlePoints, Vector2 offset)
    {
      var list = new List<SimpleParticle>();
      foreach (var point in titlePoints)
      {
        var particle = new SimpleParticle()
        {
          Position = new Vector2(r.Next(GameCore.ViewPort.Width), r.Next(GameCore.ViewPort.Height)),
          Destination = point + offset
        };

        list.Add(particle);
      }
      return list;
    }

    List<Vector2> GetParticlePositions(GraphicsDevice device, BitmapFont font, string text)
    {
      Vector2 size = font.MeasureString(text) + new Vector2(0.5f);
      int width = (int)size.X;
      int height = (int)size.Y;

      // Create a temporary render target and draw the font on it.
      RenderTarget2D target = new RenderTarget2D(device, width, height);
      device.SetRenderTarget(target);
      device.Clear(Color.Black);

      SpriteBatch spriteBatch = new SpriteBatch(device);
      spriteBatch.Begin();
      spriteBatch.DrawString(font, text, Vector2.Zero, Color.White);
      spriteBatch.End();

      device.SetRenderTarget(null);   // unset the render target

      // read back the pixels from the render target
      Color[] data = new Color[width * height];
      target.GetData(data);
      target.Dispose();

      // Return a list of points corresponding to pixels drawn by the
      // font. The font size will affect the number of points and the
      // quality of the text.
      List<Vector2> points = new List<Vector2>();
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          // Add all points that are lighter than 50% grey. The text
          // is white, but due to anti-aliasing pixels on the border
          // may be shades of grey.
          if (data[width * y + x].R > 128)
            points.Add(new Vector2(x, y));
        }
      }

      return points;
    }


    public override void Update(GameTime gameTime)
    {
      timeFromStart += (float)gameTime.ElapsedGameTime.TotalSeconds;

      if (timeFromStart > 5.0)
      {
        foreach (var particle in startParticles)
        {
          particle.Update();
        }
      }

      foreach (var particle in titleParticles)
      {
        particle.Update();
      }
    }


    public override void Draw(SpriteBatch batch)
    {
      if (timeFromStart > 5.0)
      {
        foreach (var particle in startParticles)
        {
          Vector2 pos = particle.Position * keyToStartScale;
          Vector2 origin = new Vector2(particleTexture.Width, particleTexture.Height) / 2f;
          batch.Draw(particleTexture, pos, null, Color.Green, 0f, origin, 1f, SpriteEffects.None, 0);
        }
      }

      foreach (var particle in titleParticles)
      {
        Vector2 pos = particle.Position * titleScale;
        Vector2 origin = new Vector2(particleTexture.Width, particleTexture.Height) / 2f;
        batch.Draw(particleTexture, pos, null, Color.Aqua, 0f, origin, 1f, SpriteEffects.None, 0);
      }
    }
  }

  public class SimpleParticle
  {
    public Vector2 Position { get; set; }
    public Vector2 Destination { get; set; }

    public void Update()
    {
      // move 1/60th of the way to your destination
      Position += (Destination - Position) / 60f;
    }
  }
}