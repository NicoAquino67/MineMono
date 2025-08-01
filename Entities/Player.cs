using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MineMono.Physics;


namespace MineMono.Entities
{
    public class Player
    {
        public Vector3 Position { get; private set; }
        public Vector3 Velocity { get; private set; }

        public readonly float Speed = 5f;
        public readonly float JumpStrength = 10f;

        public float Height = 2f;
        public float Radius = 0.4f;
        public int MaxHealth = 100;
        public int Health { get; private set; }

        private bool isGrounded;

        private GravitySystem gravitySystem;
        public Player(Vector3 spawnPosition, GravitySystem gravity)
        {
            Position = spawnPosition;
            Velocity = Vector3.Zero;
            Health = MaxHealth;
            gravitySystem = gravity;
        }

        public void Update(GameTime gameTime, KeyboardState input)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Basic Horizontal Movement
            Vector3 moveDir = Vector3.Zero;
            if (input.IsKeyDown(Keys.W)) moveDir += Vector3.Forward;
            if (input.IsKeyDown(Keys.S)) moveDir += Vector3.Backward;
            if (input.IsKeyDown(Keys.A)) moveDir += Vector3.Left;
            if (input.IsKeyDown(Keys.D)) moveDir += Vector3.Right;
            if (moveDir.LengthSquared() > 0) moveDir.Normalize();

            Vector3 horizontal = new(moveDir.X, 0, moveDir.Z);
            Velocity = new Vector3(horizontal.X * Speed, Velocity.Y, horizontal.Z * Speed);
            // aplica gravedad
            Velocity += gravitySystem.GetGravityForce(dt);

            if (isGrounded && input.IsKeyDown(Keys.Space)) Velocity = new Vector3(Velocity.X, JumpStrength, Velocity.Z);

            Vector3 newPos = Position + Velocity * dt;

            if (newPos.Y <= 0)
            {
                newPos.Y = 0;
                Velocity = new Vector3(Velocity.X, 0, Velocity.Z);
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

            Position = newPos;
        }
        public void TakeDamage(int dmg)
        {
            Health = MathHelper.Max(Health, dmg);
        }

        public void Heal(int amount)
        {
            Health = Math.Min(Health + amount, MaxHealth);
        }
    }
}