using System;
namespace MineMono.Blocks
{
    public struct Block
    {
        public BlockType Type;
        public Block(BlockType type)
        {
            Type = type;
        }
        //se debe renderizar?
        public bool IsSolid()
        {
            return Type != BlockType.Air;
        }
    }
}