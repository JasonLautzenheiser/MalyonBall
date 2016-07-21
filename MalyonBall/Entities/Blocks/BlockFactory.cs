using System;

namespace MalyonBall.Entities.Blocks
{
  public static class BlockFactory
  {
    public static Block CreateBlock(BlockType type)
    {
      switch (type)
      {
        case BlockType.None:
          return null;
        case BlockType.Green:
          return new GreenBlock();
        case BlockType.Red:
          return new RedBlock();
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }
    }
  }
}