using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineMono.Blocks;

namespace MineMono.World
{
    public class ChunkMesh
    {
        public List<VertexPositionColor> Vertices { get; private set; }

        private static readonly Vector3[] faceNormals = new Vector3[]
        {
            Vector3.Left, //-X
            Vector3.Right, //+X
            Vector3.Down, //-Y
            Vector3.Up, //+Y
            Vector3.Backward, //-Z
            Vector3.Forward, //+Z
        };

        //cada cara esta definida como 6 Vertices,( 2 triangulos)
        private static readonly Vector3[][] faceVertices = new Vector3[][]
        {
            //Left -x
            new Vector3[]{
                new(0,0,0), new(0,1,0), new(0,1,1),
                new(0,0,0), new(0,1,1), new(0,0,1),
            },
            //Right +x
            new Vector3[]{
                new(1,0,1), new(1,1,1), new(1,1,0),
                new(1,0,1), new(1,1,0), new(1,0,0),
            },

            //Bottom -y
            new Vector3[]{
                new(0,0,1), new(1,0,1), new(1,0,0),
                new(0,0,1), new(1,0,0), new(0,0,0),
            },
            //Top +y
            new Vector3[]{
                new(0,1,0), new(1,1,0), new(1,1,1),
                new(0,1,0), new(1,1,1), new(0,1,1),
            },

            //Back -z
            new Vector3[]{
                new(1,0,0), new(1,1,0), new(0,1,0),
                new(1,0,0), new(0,1,0), new(0,0,0),
            },
            //front +z
            new Vector3[]{
                new(0,0,1), new(0,1,1), new(1,1,1),
                new(0,0,1), new(1,1,1), new(1,0,1),
            },
        };
        public ChunkMesh(Chunk chunk)
        {
            Vertices = new List<VertexPositionColor>();
            for (int x = 0; x < Chunk.SizeX; x++)
                for (int y = 0; y < Chunk.SizeY; y++)
                    for (int z = 0; z < Chunk.SizeZ; z++)
                    {
                        var block = chunk.GetBlock(x, y, z);
                        if (!block.IsSolid()) continue;

                        for (int face = 0; face < 6; face++)
                        {
                            var offset = GetNeighborOffset(face);
                            int nx = x + offset.X;
                            int ny = y + offset.Y;
                            int nz = z + offset.Z;

                            var neighbor = chunk.GetBlock(nx, ny, nz);
                            if (neighbor.IsSolid()) continue;

                            //cara visible -> agregar vertices
                            foreach (var vert in faceVertices[face])
                            {
                                var WorldPos = new Vector3(x, y, z) + chunk.Position;
                                Vertices.Add(new VertexPositionColor(WorldPos, GetBlockColor(block.Type)));
                            }
                        }
                    }
        }
        private static Point3D GetNeighborOffset(int faceIndex)
        {
            return faceIndex switch
            {
                0 => new Point3D(-1, 0, 0), //left
                1 => new Point3D(1, 0, 0), //Right
                2 => new Point3D(0, -1, 0), //Bottom
                3 => new Point3D(0, 1, 0), //Top
                4 => new Point3D(0, 0, -1), //left
                5 => new Point3D(0, 0, 1), //left
                _ => new Point3D(0, 0, 0),
            };
        }

        private static Color GetBlockColor(BlockType type)
        {
            return type switch
            {
                BlockType.Grass => Color.Green,
                BlockType.Dirt => Color.SaddleBrown,
                BlockType.Stone => Color.Gray,
                _ => Color.Transparent,
            };
        }
    }
    public struct Point3D
    {
        public int X, Y, Z;
        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}