using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MineMono.World;

namespace MineMono.Debug
{
    public class DebugOverlay{

        private SpriteFont font;
        private bool visible = false;
        private KeyboardState prevKeyboard;

        public DebugOverlay(SpriteFont font)
        {
            this.font = font;
        }

        public void Update(GameTime gameTime)
        {
            var current = Keyboard.GetState();
            if (current.IsKeyDown(Keys.F3) && prevKeyboard.IsKeyUp(Keys.F3))
            {
                visible = !visible;
            }
            prevKeyboard = current;
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, ChunkMesh chunkMesh, Matrix view, Vector3 cameraPos)

        {
            if (!visible || font == null)
                return;

            spriteBatch.Begin();

            string[] lines = new string[]
            {
                "[DEBUG]",
                $"FPS: {(1f/gameTime.ElapsedGameTime.TotalSeconds)}",
                $"Vertices: {chunkMesh.Vertices.Count}",
                $"Chunks visibles: 1", // más adelante lo podés hacer dinámico
                $"Bloques visibles (estimado): {chunkMesh.Vertices.Count / 6}",
                $"Camera Pos: {cameraPos}",
                $"View Dir: {view.Forward}"
            };
            Vector2 pos = new Vector2(10, 10);
            foreach (var line in lines)
            {
                spriteBatch.DrawString(font, line, pos, Color.White);
                pos.Y += 20;
            }
            spriteBatch.End();
        }
    }
}