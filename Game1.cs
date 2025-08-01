using System.ComponentModel.DataAnnotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MineMono.World;
using MineMono.Debug;
using MineMono.Physics;
using MineMono.Entities;

namespace MineMono;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    GravitySystem gravity;
    Player player;

    Chunk chunk;
    ChunkMesh chunkMesh;
    BasicEffect effect;

    Matrix view;
    Matrix projection;
    DebugOverlay debugOverlay;
    SpriteFont debugFont;
    Vector3 cameraPos = new Vector3(32, 32, 64);



    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        //set a camera
        Vector3 cameraPos = new Vector3(32, 32, 64);
        Vector3 target = new Vector3(8, 8, 8); //center on the chunk

        view = Matrix.CreateLookAt(cameraPos, target, Vector3.Up);

        projection = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.ToRadians(45f),
            GraphicsDevice.Viewport.AspectRatio,
            0.1f,
            1000f
        );
        //create a chunk
        chunk = new Chunk(Vector3.Zero);
        chunkMesh = new ChunkMesh(chunk);

        //add effect Basic
        effect = new BasicEffect(GraphicsDevice);
        effect.VertexColorEnabled = true;

        //DebugScreen
        debugFont = Content.Load<SpriteFont>("DebugFont");
        debugOverlay = new DebugOverlay(debugFont);

        gravity = new GravitySystem(Vector3.Down, 20f);
        player = new Player(new Vector3(8, 10, 8), gravity);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        debugOverlay.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        effect.View = view;
        effect.Projection = projection;
        effect.World = Matrix.Identity;

        foreach (var pass in effect.CurrentTechnique.Passes)
        {
            pass.Apply();

            GraphicsDevice.DrawUserPrimitives(
                    PrimitiveType.TriangleList,
                    chunkMesh.Vertices.ToArray(),
                    0,
                    chunkMesh.Vertices.Count / 3
            );

        }
        debugOverlay.Draw(_spriteBatch, gameTime, chunkMesh, view, player.Position);
        base.Draw(gameTime);
    }
}
