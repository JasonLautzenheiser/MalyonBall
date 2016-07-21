using System;
using Microsoft.Xna.Framework.Audio;

namespace MalyonBall
{
  public enum Sound
  {
    PaddleBounce
  }

  public static class SoundManager
  {
    public static void PlaySound(Sound sound, float volume = 0.2f)
    {
      var soundEffect = getEffect(sound);
      var tsoundEffect = soundEffect.CreateInstance();

      tsoundEffect.Volume = volume;
      tsoundEffect.Play();
    }

    private static SoundEffect getEffect(Sound sound)
    {
      switch(sound)
      {
        case Sound.PaddleBounce:
          return Sounds.PaddleBounce;
        default:
          throw new ArgumentOutOfRangeException(nameof(sound), sound, null);
      }
    }
  }
}