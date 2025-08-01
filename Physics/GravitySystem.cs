using Microsoft.Xna.Framework;

namespace MineMono.Physics
{
    public class GravitySystem
    {
        public Vector3 Direction { get; set; } = Vector3.Down;
        public float Strength { get; set; } = 20f;
        public Vector3 GetGravityForce(float deltaTime)
        {
            return Direction * Strength * deltaTime;
        }
        public GravitySystem() { }
        public GravitySystem(Vector3 direction, float strength)
        {
            Direction = direction;
            Strength = strength;
        }

    }
}