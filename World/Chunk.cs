using MineMono.Blocks;
using Microsoft.Xna.Framework;
using System.CodeDom.Compiler;
using System.Runtime.ExceptionServices;
using System.Drawing;

namespace MineMono.World
{
    public class Chunk
    {
        public const int SizeX = 16;
        public const int SizeY = 16;
        public const int SizeZ = 16;
        private Block[,,] blocks = new Block[SizeX, SizeY, SizeZ];

        public Vector3 Position { get; private set; } //posicion del chunk en coordenadas del mundo

        public Chunk(Vector3 position)
        {
            Position = position;
            GenerateFlatTerrain();

        }
        private void GenerateFlatTerrain()
        {
            for (int x = 0; x < SizeX; x++)
                for (int y = 0; y < SizeY; y++)
                    for (int z = 0; z < SizeZ; z++)
                    {
                        if (y < SizeY / 2)
                            blocks[x, y, z] = new Block(BlockType.Dirt);
                        else
                            blocks[x, y, z] = new Block(BlockType.Air);
                    }
        }
        public Block GetBlock(int x, int y, int z)
        {
            if (IsInBounds(x, y, z))
                return blocks[x, y, z];
            return new Block(BlockType.Air);
        }
        public void SetBlock(int x, int y, int z, BlockType type)
        {
            if (IsInBounds(x, y, z))
                blocks[x, y, z] = new Block(type);
        }
        private bool IsInBounds(int x, int y, int z)
        {
            return x >= 0 && x < SizeX &&
                    y >= 0 && y < SizeY &&
                    z >= 0 && z < SizeZ;
        }
    }
}