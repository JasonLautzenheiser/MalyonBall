using MalyonBall.Entities.Blocks;
using Microsoft.Xna.Framework;

namespace MalyonBall
{
  public class Level
  {
    public int Number { get; set; }
    public string Name { get; set; }

    private int[,] brickMap;

    public Level()
    {
      Number = 1;
      Name = "Test Level #1";

      brickMap = new[,]
      {
        {1, 0, 1, 0, 1, 1, 1, 0, 2, 0, 0, 0, 2, 0, 0, 0, 1, 0, 1, 0},
        {1, 0, 1, 0, 1, 0, 1, 0, 2, 0, 0, 0, 2, 0, 0, 0, 1, 0, 1, 0},
        {1, 1, 1, 0, 1, 0, 1, 0, 2, 0, 0, 0, 2, 0, 0, 0, 0, 1, 0, 0},
        {1, 0, 1, 0, 1, 0, 1, 0, 2, 0, 0, 0, 2, 0, 0, 0, 0, 1, 0, 0},
        {1, 0, 1, 0, 1, 1, 1, 0, 2, 2, 2, 0, 2, 2, 2, 0, 0, 1, 0, 0},
      };

//      brickMap = new[,]
//      {
//        {1, 1, 1, 0, 1, 1, 1, 0, 2, 0, 2, 0, 1, 1, 1, 0, 1, 0, 0, 1},
//        {1, 0, 0, 0, 0, 1, 0, 0, 2, 0, 2, 0, 1, 0, 1, 0, 1, 1, 0, 1},
//        {1, 1, 0, 0, 0, 1, 0, 0, 2, 2, 2, 0, 1, 1, 1, 0, 1, 0, 1, 1},
//        {1, 0, 0, 0, 0, 1, 0, 0, 2, 0, 2, 0, 1, 0, 1, 0, 1, 0, 0, 1},
//        {1, 1, 1, 0, 0, 1, 0, 0, 2, 0, 2, 0, 1, 0, 1, 0, 1, 0, 0, 1},
//      };

    }

    public void Load()
    {
      for (int row = 0; row < brickMap.GetLength(0); row++)
      {
        for (int col = 0; col < brickMap.GetLength(1); col++)
        {
          var brick = brickMap[row,col];
          if (brick != 0)
          {
            Block block = BlockFactory.CreateBlock((BlockType)brick);
            block.Position = new Vector2(64*col + 50, 100 + row*20);
            GameCore.Instance.EntityManager.AddEntity(block);
          }
        }
      }

    }
  }
}