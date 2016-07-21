using System.Collections.Generic;

namespace MalyonBall
{
  public class LevelManager
  {
    public List<Level> Levels { get; set; }

    public LevelManager()
    {
      Levels = new List<Level>();
      var level = new Level();

      Levels.Add(level);
    }

    public void LoadLevel()
    {
      var currentLevel = Levels[0];

      currentLevel.Load();


    }
  }
}