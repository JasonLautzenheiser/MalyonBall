using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MalyonBall
{
  public static class Extensions
  {
    public static Vector2 ScaleTo(this Vector2 vector, float length)
    {
      return vector * (length / vector.Length());
    }

    public static Vector2 NextVector2(this FastRandom rand, float minLength, float maxLength)
    {
      double theta = rand.NextSingle() * 2 * Math.PI;
      float length = rand.NextSingle(minLength, maxLength);
      return new Vector2(length * (float)Math.Cos(theta), length * (float)Math.Sin(theta));
    }
  }
}