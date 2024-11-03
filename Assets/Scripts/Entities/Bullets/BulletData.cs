using UnityEngine;

namespace Entities.Bullets
{
    public struct BulletData
    {
        public Vector2 Position { get; }

        public Vector2 Velocity { get; }
        
        public Color Color  { get; }

        public BulletData(Vector2 position, Vector2 velocity, Color color)
        {
            Position = position;
            Velocity = velocity;
            Color = color;
        }
    }
}